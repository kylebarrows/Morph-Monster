using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Health playerHealth;
    private Health enemyHealth;
    private EnemyPatrol enemyPatrol;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject corpse;

    private bool isDead => enemyHealth.currentHealth <= 0;

    private Animator anim;

    private bool m_isActive;

    private void Awake()
    {
        enemyHealth = GetComponent<Health>();
        m_isActive = true;
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        MonsterController.OnPlayerDie += RespawnEnemy;
    }

    private void OnDestroy()
    {
        MonsterController.OnPlayerDie -= RespawnEnemy;
    }

    void Update()
    {
        if(!m_isActive)
        {
            return;
        }

        if(isDead)
        {
            Die();
        }

        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("MeleeAttack");
                // DamagePlayer();
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight()
    {
        Vector3 gizmosSize = new Vector3(1.2f, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x, gizmosSize,
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.collider.gameObject.GetComponent<Health>();

        return hit.collider != null;
    }

    private void Die()
    {
        anim.SetTrigger("Die");
        m_isActive = false;
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        ConsumableObject co = gameObject.GetComponent<ConsumableObject>();
        if (!co.isConsumed)
        {
            // Spawn a corpse
            Instantiate(corpse, transform.position + Vector3.up, Quaternion.Euler(0, 0, 90));
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 gizmosSize = new Vector3(1.2f, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x, gizmosSize);
        // Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range, gizmosSize);
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }

    private void RespawnEnemy()
    {
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
        enemyHealth.Respawn();
        m_isActive = true;
    }


}
