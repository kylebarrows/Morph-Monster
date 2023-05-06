using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossScript : MonoBehaviour
{
    [Header("Boss Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float attackCooldown;
    //[SerializeField] private float spellCooldown;
    //[SerializeField] private float teleportCooldown;
    [SerializeField] private float BehaviorCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private int teleportRange;


    [Header("Technical Fields")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject spell;


    private float cooldownTimer1 = Mathf.Infinity;
    private float cooldownTimer2 = Mathf.Infinity;
    private float cooldownTimer3 = Mathf.Infinity;
    private float cooldownTimer4 = Mathf.Infinity;

    private Health playerHealth;
    private Health enemyHealth;

    private bool isDead => enemyHealth.currentHealth <= 0;

    private Animator anim;
    private GameObject player;
    private Transform enemyPos;
    private Transform playerPos;
    private Vector3 initScale;
    private bool moveFlag;
    private int behaviorControl;
    //Random rnd = new Random();

    private bool m_isActive;
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public AudioClip audioClip3;
    public AudioClip audioClip4;
    AudioSource telesound;
    AudioSource surprisesound;
    AudioSource thundersound;
    AudioSource smashsound;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        enemyHealth = GetComponent<Health>();
        playerHealth = player.GetComponent<Health>();

        enemyPos = GetComponent<Transform>();
        playerPos = player.GetComponent<Transform>();
        initScale = enemyPos.localScale;

        m_isActive = true;
        anim = GetComponent<Animator>();
        behaviorControl = Random.Range(1, 4);
        telesound = AddAudio (0.6f);
        surprisesound = AddAudio (0.3f);
        thundersound = AddAudio (0.6f);
        smashsound = AddAudio (0.4f);
    }

    void Update()
    {
        if (!m_isActive)
        {
            return;
        }

        if (isDead)
        {
            Die();
        }

        if (enemyPos.position.x >= playerPos.position.x)
            enemyPos.localScale = new Vector3(Mathf.Abs(initScale.x),
        initScale.y, initScale.z);
        else
            enemyPos.localScale = new Vector3(Mathf.Abs(initScale.x) *-1,
        initScale.y, initScale.z);

        cooldownTimer1 += Time.deltaTime;
        cooldownTimer4 += Time.deltaTime;

        chargeBehavior();


        if (cooldownTimer4 >= BehaviorCooldown)
        {
            cooldownTimer4 = 0;
            behaviorControl = Random.Range(1, 3);

            if (behaviorControl == 1)
                spellBehavior();
            if (behaviorControl == 2 && Vector3.Distance(playerPos.position, enemyPos.position) < teleportRange)
                teleportBehavior();

        }
    }

    private void teleportBehavior()
    {
        //cooldownTimer3 += Time.deltaTime;

        //if (cooldownTimer3 >= teleportCooldown)
        //{
            cooldownTimer3 = 0;
            anim.SetTrigger("Teleport");
            telesound.clip = audioClip1;
            telesound.Play ();
            surprisesound.clip = audioClip2;
            surprisesound.Play ();
            // DamagePlayer();
        //}
    }

    private void teleport()
    {
        int flag = Random.Range(0, 2);
        int dis = 5;

        if (flag == 0)
            dis *= -1;

        transform.position = new Vector2(playerPos.position.x + dis, transform.position.y);
        chargeBehavior();
    }

    private void tpAttack()
    {
        anim.SetTrigger("Attack");
        smashsound.clip = audioClip4;
        smashsound.Play ();
    }

    private void chargeBehavior()
    {
        if (PlayerInSight())
        {
            if (cooldownTimer1 >= attackCooldown)
            {

                cooldownTimer1 = 0;
                anim.SetTrigger("Attack");
                smashsound.clip = audioClip4;
                smashsound.Play ();
                // DamagePlayer();
            }
        }
        

    }

    private void spellBehavior()
    {
        //cooldownTimer2 += Time.deltaTime;

        //if (cooldownTimer2 >= spellCooldown)
        //{
            cooldownTimer2 = 0;
            anim.SetTrigger("Spell");
            // DamagePlayer();
        //}

    }

    private void spawnSpell()
    {
        Vector3 pos = new Vector3(playerPos.position.x + 3, playerPos.position.y + (float)2.5, playerPos.position.z);
        Instantiate(spell, pos, Quaternion.identity);
        thundersound.clip = audioClip3;
        thundersound.Play ();
    }


    private void setIdle()
    {
        anim.SetBool("Moving", false);
        anim.SetTrigger("Idle");
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * -1, boxCollider.bounds.size,
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void Die()
    {

        anim.SetTrigger("Die");
        m_isActive = false;
        SceneManager.LoadScene(5); 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * -1, boxCollider.bounds.size);
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }
        public AudioSource AddAudio(float vol) 
    { 
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.playOnAwake = false;
        newAudio.volume = vol; 
        return newAudio; 
     }



}