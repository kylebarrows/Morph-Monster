using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour, IMonsterReceiver
{ 
    [SerializeField] public float GRAVITY_SCALE;

    public MonsterCharacter Character;

    public MonsterController Controller;

    public float CarryOverXSpeed;

    public Vector2 LocalSpeed;

    [SerializeField] private LayerMask platformLayer;

    public Vector2 prevFrameVelocity;
    private Vector2 additionalVelocity;
    private bool hasAdditionalVelocity;

    //public static int platformLayer = 8;

    public bool ceiling => CheckForCeiling();

    public bool jumping => LocalSpeed.y > 0f;

    public bool isOnGround;

    public bool willBeOnGround;
    public bool isFalling => !isOnGround && Controller.rb2d.velocity.y < 0f;

    public bool isInClimbZone;

    public ClimbableBehavior Climbable;
    
    public bool isOnPlatform;

    public bool willBeOnPlatform;

    public bool isInPlatform => CheckIfInPlatform();

    public Vector2 movingPlatformVelocity { get; set; }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Climbable")
        {
            Climbable = collision.gameObject.GetComponent<ClimbableBehavior>();
            isInClimbZone = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Climbable")
        {
            Climbable = null;
            isInClimbZone = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision(collision);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        OnCollision(collision);
    }

    public void OnCollision(Collision2D collision)
    {
        for(int i = 0; i < collision.contacts.Length; i++)
        {
            ContactPoint2D contactPoint = collision.contacts[i];
            Vector2 vector = contactPoint.normal;

            // Check if collision is on ground
            if(Vector2.Dot(vector, Vector2.up) >= Mathf.Cos((float)Mathf.PI / 180f * 5f))
            {
                willBeOnGround = true;
            }

            if(collision.gameObject.tag == "Platform")
            {
                willBeOnPlatform = true;
            }
        }
    }

    private bool CheckForCeiling()
    {
        Vector2 vector = Controller.collider.bounds.center;
        float distance = Controller.collider.bounds.extents.y + 0.6f;
        
        RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, Vector2.up, distance, Controller.groundLayer);
        Debug.DrawLine(vector, new Vector2(vector.x, vector.y + distance), Color.cyan);
        if (raycastHit2D.distance > 0)
        {
            if (raycastHit2D.collider.GetComponent<OneWayPlatform>())
            {
                return false;
            }
            return true;
        }
        return false;
    }

    private bool CheckIfInPlatform()
    {
        Vector2 vector = new Vector2(Controller.collider.bounds.center.x, Controller.collider.bounds.max.y);
        float distance = vector.y - Controller.collider.bounds.min.y - 0.4f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, Vector2.down, distance, platformLayer);
        if(raycastHit2D)
        {
            Debug.Log("InPlatform");
            return true;
        }
        return false;
    }

    private void UpdateCollisions()
    {
        isOnGround = willBeOnGround;
        willBeOnGround = false;

        isOnPlatform = true;
        willBeOnPlatform = false;
    }

    public void FixedUpdate()
    {
        LocalSpeed = Controller.rb2d.velocity;
        LocalSpeed = new Vector2(Mathf.Abs(LocalSpeed.x), Mathf.Abs(LocalSpeed.y));

        if(isFalling)
        {
            Controller.rb2d.gravityScale = 2 * GRAVITY_SCALE;
        }
        else if(Character.Abilities.Climb.isClimbing)
        {
            Controller.rb2d.gravityScale = 0f;
        }
        else
        {
            Controller.rb2d.gravityScale = GRAVITY_SCALE;
        }

        Controller.animator.SetFloat("Airspeed Y", Controller.rb2d.velocity.y);
        UpdateCollisions();
        CheckIfInPlatform();
    }

    public void SetMonsterReference(MonsterCharacter character)
    {
        Debug.Log("Received!!!");
        Character = character;
        Controller = Character.Controller;
    }
}
