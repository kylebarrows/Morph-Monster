using System;
using UnityEngine;

public class MonsterController : FormBase, IMonsterReceiver
{
    // Togglable through editor for now to disable features
    [SerializeField] public bool canLedgeClimbOverride;
    [SerializeField] public bool canClimbOverride;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ledgeCheck; // Maybe make this ledge check offsets?
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public LayerMask platformLayer;

    public Vector2 prevFrameVelocity;
    private Vector2 conservedVelocity;
    private bool hasPreservedVelocity;

    private MonsterCharacter Character;

    private Health monsterHealth;

    public MonsterInput MInput;

    public MovementHandler MovementHandler;

    public Rigidbody2D rb2d;

    public CapsuleCollider2D collider;

    public Animator animator;

    private static MonsterController _instance;

    private DamageMode damageMode;

    GameData gameData = new GameData();

    public RespawnPost respawnPost { get; set; }

    public static event Action OnPlayerDie;

    public bool isOnGround
    {
        get
        {
            if(MovementHandler.LocalSpeed.y > 0.05f)
            {
                //return false;
            }
            return MovementHandler.isOnGround;
        }
    }

    public bool isPullingObject => Character.Abilities.PushPull.isPulling;

    public bool facingRight;

    public static MonsterController instance
    {
        get
        {
            MonsterController silentInstance = SilentInstance;
            if(!silentInstance)
            {
                Debug.LogError("Couldn't find a Hero, make sure one exists!");
            }
            return silentInstance;
        }
    }

    public static MonsterController SilentInstance
    {
        get
        {
            if(_instance == null)
            {
                _instance = UnityEngine.Object.FindObjectOfType<MonsterController>();
                if((bool)_instance && Application.isPlaying)
                {
                    UnityEngine.Object.DontDestroyOnLoad(_instance.gameObject);
                }
            }
            return _instance;
        }
    }

    public void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            UnityEngine.Object.DontDestroyOnLoad(this);
        }
        else if(this != _instance)
        {
            UnityEngine.Object.Destroy(base.gameObject);
            return;
        }

        SetupGameObjects();
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 17, true);
        Character.Abilities.GetAbility("Jump");
        MonsterFormAbility.Activate(Character.Abilities.Jump);
        MonsterFormAbility.Activate(Character.Abilities.Run);
        MonsterFormAbility.Activate(Character.Abilities.Climb);
        MonsterFormAbility.Activate(Character.Abilities.Consume);
        MonsterFormAbility.Activate(Character.Abilities.Clamber);
    }

    public void Reset()
    {
        respawnPost = null;
    }

    private void SetupGameObjects()
    {
        // Test to see if we can add components as needed at run time
        // MonsterJump Jump2 = gameObject.AddComponent(typeof(MonsterJump)) as MonsterJump;
        MInput = GetComponent<MonsterInput>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
        MovementHandler = GetComponent<MovementHandler>();
        monsterHealth = GetComponent<Health>();
    }

    private void ClimbLedge()
    {
        Vector2 vector = ledgeCheck.position;
        Debug.DrawRay(vector, Vector2.down, Color.red);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, Vector2.down, 1f, groundLayer);
        if(raycastHit2D.distance > 0)
        {
            Vector2 newPosition = raycastHit2D.point;
            newPosition.y += collider.bounds.center.y;
            // Check point. Ensure it's valid (yeah man)
            // Need a climb animation, we'll teleport the player to the new position when its done
            // but for now we'll just teleport player to the position
            transform.position = newPosition;
        }
    }

    private void ClimbThroughPlatform()
    {
        Vector2 vector = collider.bounds.center;
        Debug.DrawLine(vector, new Vector2(vector.x, vector.y + 1f), Color.blue);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, Vector2.up, 1f);
        if (raycastHit2D.distance > 0)
        {
            rb2d.velocity = Vector2.zero;
            Vector2 newPosition = raycastHit2D.point;
            newPosition.y += collider.bounds.center.y;
            transform.position = newPosition;
        }
    }

    public void PreserveVelocityForNextFrame(Vector2 prevVel)
    {
        hasPreservedVelocity = true;
        conservedVelocity = prevVel;
    }

    private void ApplyPreservedVelocity()
    {
        hasPreservedVelocity = false;
        rb2d.velocity = conservedVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOnGround)
        {
           animator.SetBool("Grounded", isOnGround);
        }
        else
        {
           animator.SetBool("Grounded", isOnGround);
        }

        if(MInput.verticalDir != 0f)
        {
            if (MInput.verticalDir < 0f && isOnGround)
            {
                Character.Abilities.Climb.StopClimbing();
            }
            else
            {
                Character.Abilities.Climb.Climb();
            }
        }

        if(MInput.consumeHeld)
        {
            MonsterFormAbility.Deactivate(Character.Abilities.Run);
            Character.Abilities.Run.StopRunning();
        }
        else
        {
            MonsterFormAbility.Activate(Character.Abilities.Run);
            animator.SetBool("ConsumeHeld", false);
        }
        
        if (canLedgeClimbOverride)
        {
            ClimbLedge();
        }
    }

    private void FixedUpdate()
    {
        if(hasPreservedVelocity)
        {
            ApplyPreservedVelocity();
        }

        if (!Character.Abilities.Climb.isClimbing)
        {
            Character.Abilities.Run.Move();
        }

        HandleJumping();

        if(MInput.consumeHeld)
        {
            //Character.Abilities.Consume.StartConsume();
        }

        if(MInput.attackHeld)
        {
            Character.Abilities.Attack.Attack();
        }
        
        if(MInput.ejectFormPressed)
        {
            Character.Abilities.Jump.PerformFormChangeJump();
            Character.ChangeForm(0, false);
        }

        if(monsterHealth.currentHealth <= 0)
        {
            Die();
        }

        prevFrameVelocity = rb2d.velocity;

        Character.Abilities.PushPull.UpdateAbility();
        Character.Abilities.Jump.UpdateAbility();
        Character.Abilities.Consume.UpdateAbility();
        Character.Abilities.Clamber.UpdateAbility();
        Character.Abilities.Shoot.UpdateAbility();
    }

    public void HandleJumping()
    {
        if(MInput.downJumpPressed)
        {

        }
        else if (MInput.jumpHeld && !MInput.consumeHeld && !isPullingObject)
        {
            if (GetComponent<FormSwapHandler>().activeForm.Form == FormNames.Brute)
                return;
            Character.Abilities.Climb.StopClimbing();
            Character.Abilities.Jump.PerformJump();
            Character.Abilities.Jump.PerformHoldJump();
        }
    }

    public void SetColliderBounds(Vector2 size, Vector2 offset)
    {
        collider.size = size;
        collider.offset = offset;
    }

    public void SetCharacterScale(Vector2 scale)
    {
        transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x) * scale.x, scale.y, transform.localScale.z);
    }

    private void SetDamageMode(DamageMode newDamageMode)
    {
        damageMode = newDamageMode;
    }

    // Returns the position of a climbable ledge if found
    private Vector2 FindClimbableLedge()
    {
        Vector2 vector = ledgeCheck.position;
        Debug.DrawRay(vector, Vector2.down, Color.red);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, Vector2.down, 1f, groundLayer);
        if (raycastHit2D && raycastHit2D.distance > 0)
        {
            Debug.Log("found legde");
            return raycastHit2D.point;
        }

        return Vector2.zero;
    }

    // Check if there is a ceiling above the player
    
    
    // Check if there is a platform below the player
    public bool CheckForPlatformBelow()
    {
        Vector2 vector = collider.bounds.center;
        float distance = collider.bounds.extents.y + 0.16f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, Vector2.down, distance, groundLayer);
        if(raycastHit2D.collider.GetComponent<OneWayPlatform>())
        {
            return true;
        }

        return false;
    }

    // Check if the player is touching the ground
    

    public void TakeDamage(GameObject go, float damageAmount)
    {
        if(damageAmount <= 0)
        {
            return;
        }
        Health health = GetComponent<Health>();
        if(health)
        {
            health.TakeDamage(damageAmount);
            Debug.Log("Player health: ");
        }
    }

    public bool CanTakeDamage()
    {
        if(damageMode != DamageMode.NO_DAMAGE)
        {
            return true;
        }
        return false;
    }

    public void RespawnAtLastPost()
    {
        if(respawnPost)
        {
            transform.position = respawnPost.transform.position;
        }
        else
        {
            GameObject startGO = GameObject.FindWithTag("StartPosition");
            if (startGO)
            {
                transform.position = startGO.transform.position;
            }
        }
    }

    public void Die()
    {
        OnPlayerDie.Invoke();

        monsterHealth.Respawn();
        LoadState();
        RespawnAtLastPost();
    }

    public void LoadState()
    {
        Character.SwapHandler.ChangeForm(gameData.playerFormID, true);
    }

    public void SaveState()
    {
        gameData.playerFormID = Character.ActiveFormId;
        gameData.playerPosition = Character.transform.position;
    }

    public void SetMonsterReference(MonsterCharacter character)
    {
        Character = character;
    }
}
