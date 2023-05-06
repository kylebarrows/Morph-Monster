using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    public enum PlatformTypes { Upward, Downward, Both }
    public PlatformTypes type = PlatformTypes.Both;

    [SerializeField] private float delay = 0.5f;
    [SerializeField] private static LayerMask PlayerMask;
    [SerializeField] private static LayerMask PlatformLayer;

    private Collider2D collider;
    private GameObject player;
    private CapsuleCollider2D playerCollider;

    void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        collider = GetComponent<Collider2D>();
        player = FindObjectOfType<MonsterController>().gameObject;
        playerCollider = player.GetComponent<CapsuleCollider2D>();
        PlayerMask = player.layer;
        PlatformLayer = 8;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(player == null)
        {
            Setup();
        }

        if(collision.gameObject == player)
        {
            if(playerCollider.bounds.min.y < collider.bounds.center.y && player.GetComponent<MonsterController>().MovementHandler.jumping && type != PlatformTypes.Downward)
            {
                //Physics2D.IgnoreLayerCollision(PlayerMask, PlatformLayer, true);
                Physics2D.IgnoreCollision(playerCollider, collider, true);
                StartCoroutine(StopIgnoring());

                player.GetComponent<MonsterController>().PreserveVelocityForNextFrame(player.GetComponent<MonsterController>().prevFrameVelocity);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            if(player.GetComponent<MonsterController>().MInput.downJumpPressed && playerCollider.bounds.min.y > collider.bounds.center.y && type != PlatformTypes.Upward)
            {
                Physics2D.IgnoreCollision(playerCollider, collider, true);
                StartCoroutine(StopIgnoring());
            }
        }
    }

    private IEnumerator StopIgnoring()
    {
        yield return new WaitForSeconds(delay);
        Physics2D.IgnoreCollision(playerCollider, collider, false);
    }
}
