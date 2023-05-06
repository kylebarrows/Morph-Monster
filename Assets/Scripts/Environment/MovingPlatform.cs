using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float m_Speed = 1.0f;
    [SerializeField] private float x_TravelDistance = 0.0f;

    [Range(-1.0f, 1.0f)]
    [SerializeField] private float x_TravelDirection = 1.0f;

    [SerializeField] private float endPointWaitTime = 1.0f;
    private float waitTimer = 0.0f;
    private float speed = 0.0f;

    private float travelTime = 0.0f;
    private float travelTimer = 0.0f;

    private Vector2 startingPos;
    private Vector2 endPointPos;

    private Rigidbody2D rb2d;

    private bool isActive = true;
    private bool bTravelback = false;
    private bool bWaiting = false;
    private bool bTraveling = true;
    [SerializeField] private bool bWaitingForPlayerToStartMoving = false;
    private bool waitForPlayer;
    [SerializeField] private bool bTravelVertically = false;

    void Start()
    {

    }

    private void OnDestroy()
    {
        MonsterController.OnPlayerDie -= ResetPosition;
    }

    private void Setup()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0.0f;

        startingPos = transform.position;
        if (bTravelVertically)
        {
            endPointPos = new Vector2(startingPos.x, (x_TravelDirection * x_TravelDistance) + startingPos.y);
        }
        else
        {
            endPointPos = new Vector2(startingPos.x + (x_TravelDirection * x_TravelDistance), startingPos.y);
        }


        // Ensure starting point is less than endpoint for checking sake
        if (startingPos.x > endPointPos.x)
        {
            startingPos = endPointPos;
            endPointPos = transform.position;
        }

        speed = x_TravelDistance * m_Speed;
        travelTime = x_TravelDistance / speed;
        StartMoving();

        waitForPlayer = bWaitingForPlayerToStartMoving;
    }

    private void Awake()
    {
        Setup();
        StartMoving();

        waitForPlayer = bWaitingForPlayerToStartMoving;
        MonsterController.OnPlayerDie += ResetPosition;
    }

    private void StartMoving()
    {
        bTraveling = true;
        bWaiting = false;
        travelTimer = travelTime;
    }

    private void Move()
    {
        if (bTravelback)
        {
            rb2d.velocity = new Vector2(-speed, 0);
            if(bTravelVertically)
            {
                rb2d.velocity = new Vector2(0, -speed);
            }
        }
        else
        {
            rb2d.velocity = new Vector2(speed, 0);
            if (bTravelVertically)
            {
                rb2d.velocity = new Vector2(0, speed);
            }
        }
    }

    private void ResetPosition()
    {
        waitForPlayer = bWaitingForPlayerToStartMoving;
        transform.position = startingPos;
        bTraveling = false;
        bTravelback = false;

        rb2d.velocity = Vector2.zero;
        waitTimer = travelTimer = 0.0f;

    }

    private bool ShouldUpdatePlayerVelocity(Vector3 playerPos)
    {
        if(bTravelVertically)
        {
            return false;
        }

        Vector2 relativePos = playerPos - transform.position;

        if(playerPos.y > transform.position.y)
        {
            return true;
        }
        // Check if the platform is moving away from the player
        float rDotV = Vector2.Dot(relativePos, rb2d.velocity);
        if(rDotV < 0.0f)
        {
            return false;
        }

        return true;
    }

    private void OnEndPointArrive()
    {
        bWaiting = true;
        bTraveling = false;
        rb2d.velocity = Vector2.zero;
        waitTimer = endPointWaitTime;

        bTravelback = !bTravelback;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(waitForPlayer)
        {
            waitForPlayer = false;
        }

        if(collision.gameObject.tag == "Player")
        {
            GameObject go = collision.gameObject;
            MovementHandler rb = go.GetComponent<MovementHandler>();
            if (ShouldUpdatePlayerVelocity(go.transform.position))
            {
                rb.movingPlatformVelocity = rb2d.velocity;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject go = collision.gameObject;
            MovementHandler rb = go.GetComponent<MovementHandler>();
            rb.movingPlatformVelocity = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive || waitForPlayer)
        {
            return;
        }

        if(bTraveling)
        {
            Move();
        }

        if (waitTimer > 0.0f)
        {
            waitTimer -= Time.deltaTime;
        }
        else
        {
            if (!bTraveling)
            {
                StartMoving();
            }
        }

        if (travelTimer > 0.0f)
        {
            travelTimer -= Time.deltaTime;
        }
        else
        {
            if (!bWaiting)
            {
                OnEndPointArrive();
            }
        }
    }
}
