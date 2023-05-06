using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    private GameObject player;
    //private Animator anim;
    private Health playerHealth;
    private Transform playerPos;
    private Transform spellPos;
    [SerializeField] private float damage;
    [SerializeField] private float hitboxSize;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //anim = GetComponent<Animator>();
        playerPos = player.GetComponent<Transform>();
        playerHealth = player.GetComponent<Health>();
        spellPos = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }

    }

    private void destroy()
    {
        Destroy(gameObject);
    }

    private void damagePlayer()
    {
        if (playerInRange())
            playerHealth.TakeDamage(damage);
    }

    private bool playerInRange()
    {
        if (playerPos.position.y > (spellPos.position.y - 4) && playerPos.position.y < spellPos.position.y
            && playerPos.position.x > (spellPos.position.x - hitboxSize - 3) && playerPos.position.x < (spellPos.position.x + hitboxSize-3))
            return true;
        else
            return false;
    }
}
