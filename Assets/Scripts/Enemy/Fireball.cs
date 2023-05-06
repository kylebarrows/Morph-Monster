using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float projectileLife;
    [SerializeField] public float damage;

    private GameObject player;
    private Rigidbody2D rb;

    private double timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreLayerCollision(9, 7, true);
        Physics2D.IgnoreLayerCollision(9, 9, true);
        Physics2D.IgnoreLayerCollision(9, 17, true);
        Physics2D.IgnoreLayerCollision(9, 18, true);


        Vector3 direction = player.transform.position - transform.position;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;

        float rot = Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rot * -1);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * 3.3;

        if (timer > projectileLife)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MonsterController mc = collision.gameObject.GetComponent<MonsterController>();
        if (mc)
        {
            //mc.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
