using System;
using UnityEngine;

public class PushPullObject : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb2d;

    [SerializeField] private float DISTANCE_TO_PLAYER;
    [SerializeField] private LayerMask groundLayer;

    private float m_distance;

    private bool isAttached;

    [SerializeField] public PhysicsMaterial2D PushPullMaterial;

    [SerializeField] public PhysicsMaterial2D PushPullMaterialGrabbed;

    private Rigidbody2D characterRb;

    public static event Action OnDetachFromCharacter;

    public float halfWidth;

    private bool isFalling;

    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        halfWidth = CalculateBoxWidth();
        isAttached = false;
        m_distance = DISTANCE_TO_PLAYER + CalculateBoxWidth();
    }

    public void OnGrabbed()
    {
        isAttached = true;
        rb2d.sharedMaterial = PushPullMaterialGrabbed;
        Physics.IgnoreLayerCollision(6, 18, true);
    }

    public void OnReleased(Rigidbody2D rb)
    {
        rb2d.velocity = rb.velocity;
        isAttached = false;
        rb2d.sharedMaterial = PushPullMaterial;
        Physics.IgnoreLayerCollision(6, 18, false);
    }

    public void OnMove(Rigidbody2D rb)
    {
        if(!isFalling)
        {
            rb2d.velocity = rb.velocity;
            //UpdatePosition(rb);
        }
    }

    private float CalculateBoxWidth()
    {
        BoxCollider2D boxCollider = rb2d.GetComponent<BoxCollider2D>();
        float halfBoxWidth = boxCollider.bounds.max.y - boxCollider.bounds.center.y;
        return halfBoxWidth;
    }

    public void AttachToPlayer(Rigidbody2D character)
    {
        characterRb = character;
        isAttached = true;
    }

    private void UpdatePosition(Rigidbody2D rb)
    {
        if (isAttached && !isFalling)
        {
            Vector2 start = transform.position;
            Vector2 end;
            // Check if we are to the left or right of the object
            if (characterRb.transform.position.x < start.x)
            {
                end = new Vector2(rb.transform.position.x + m_distance,
                    start.y);
            }
            else
            {
                end = new Vector2(rb.transform.position.x - m_distance,
                    start.y);
            }

            transform.position = end;
        }
    }

    private bool IsFloorBelow()
    {
        Vector2 start = transform.position;
        Debug.DrawLine(start, new Vector2(transform.position.x, transform.position.y - 3), Color.cyan);
        RaycastHit2D hit = Physics2D.Raycast(start, Vector2.down, 
            m_distance - DISTANCE_TO_PLAYER + 0.05f, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //UpdatePosition(characterRb);
        isFalling = !IsFloorBelow();
    }
}
