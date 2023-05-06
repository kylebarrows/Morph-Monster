using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30f;
    private Rigidbody2D rb2d;
    private Collider2D collider;

    [SerializeField] private ParticleSystem destroyEffect;

    public float maxLifeTime = 10f;
    private float damage = 1.0f;

    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    // Handles setting paramters on projectile spawn
    // collider, the collider of the object that spawned this projectile
    // direction, the direction the projectile should travel
    public void SpawnProjectile(Collider2D makerCollider, Vector2 direction, float damage)
    {
        // set the projectiles speed and velocity
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = speed * direction;
        rb2d.gravityScale = 0.0f;

        // set collision response so it ignores collision with creator
        collider = GetComponent<Collider2D>();
        if(!collider)
        {
            collider = gameObject.AddComponent<CircleCollider2D>();
        }
        Physics2D.IgnoreCollision(makerCollider, collider, true);

        this.damage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        destroyEffect.Play();
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.enabled = false;
        rb2d.bodyType = RigidbodyType2D.Static;
        Destroy(gameObject, 0.3f);

        GameObject go = collision.gameObject;
        Health health = go.GetComponent<Health>();
        if(health)
        {
            health.TakeDamage(damage);
        }
    }

    public void OnConsume()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
