using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private GameObject fireball;
    [SerializeField] private Transform fireballPos;
    [SerializeField] private float cooldown;
    [SerializeField] private float range;
    [SerializeField] private GameObject corpse;

    private float timer;
    private GameObject player;
    private Animator anim;
    private Transform enemyPos;
    private Transform playerPos;
    private Vector3 initScale;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private Health health;
    private bool m_isActive;
    public AudioClip audioClip1;
    AudioSource fireballsound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();

        enemyPos = GetComponent<Transform>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        initScale = enemyPos.localScale;
        fireballsound = AddAudio (0.3f);
    }

    private void OnDestroy()
    {
        MonsterController.OnPlayerDie -= RespawnEnemy;
    }

    private void Awake()
    {
        m_isActive = true;
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        MonsterController.OnPlayerDie += RespawnEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_isActive)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (enemyPos.position.x <= playerPos.position.x)
            enemyPos.localScale = new Vector3(Mathf.Abs(initScale.x), initScale.y, initScale.z);
        else
            enemyPos.localScale = new Vector3(Mathf.Abs(initScale.x) * -1, initScale.y, initScale.z);

        if (distance < range)
        {
            
            timer += Time.deltaTime;

            if (timer > cooldown)
            {
                anim.SetTrigger("RangedAttack");
                timer = 0;
                //shoot();
            }
        }

        if(health.currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        m_isActive = false;
        spriteRenderer.enabled = false; 
        boxCollider.enabled = false;
        ConsumableObject co = gameObject.GetComponent<ConsumableObject>();
        if(!co.isConsumed)
        {
            // Spawn a corpse
            Instantiate(corpse, transform.position + Vector3.up, Quaternion.Euler(0, 0, 90));
        }
    }

    void shoot()
    {
        Instantiate(fireball, fireballPos.position, Quaternion.identity);
        fireballsound.clip = audioClip1;
        fireballsound.Play ();
    }

    private void RespawnEnemy()
    {
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
        health.Respawn();
        m_isActive = true;
    }
    public AudioSource AddAudio(float vol) 
    { 
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.playOnAwake = false;
        newAudio.volume = vol; 
        return newAudio; 
     }
}
