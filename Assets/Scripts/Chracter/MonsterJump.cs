using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterJump : MonsterFormAbility, IMonsterReceiver
{
    public MonsterCharacter Character;

    public MonsterController Controller;

    [SerializeField] private float JUMP_HEIGHT;
    [SerializeField] private float NUM_JUMPS;
    [SerializeField] private float JUMP_COOLDOWN;
    [SerializeField] private float COYOTE_TIME;
    [SerializeField] private float JUMP_HOLD_TIME;

    // Timers
    private float coyoteTimeCounter;
    private float jumpTimer;
    private float jumpsRemaining;
    private float jumpHoldTimer;
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    
    AudioSource jumpsound;
    AudioSource ejectsound;

    public bool CanJump => jumpsRemaining >= 1 && coyoteTimeCounter > 0 && !Controller.MovementHandler.ceiling;

    void Start()
    {
        FormSwapHandler.OnFormChange += OnFormChange;
        jumpsound = AddAudio (0.6f);
        ejectsound = AddAudio (0.5f);
    }

    public void PerformJump()
    {

        if (!Active || !CanJump)
        {
            return;
        }

        jumpsound.clip = audioClip1;
        jumpsound.Play ();

        Controller.animator.SetTrigger("Jump");
        Controller.animator.SetBool("Grounded", false);
        --jumpsRemaining;
        coyoteTimeCounter = 0f;
        jumpTimer = JUMP_COOLDOWN;
        jumpHoldTimer = JUMP_HOLD_TIME;
        Controller.rb2d.velocity = new Vector2(Controller.rb2d.velocity.x, JUMP_HEIGHT);
    }

    public void PerformHoldJump()
    {
        if (!Active)
        {
            return;
        }

        if (jumpHoldTimer > 0f)
        {
            Controller.rb2d.velocity = new Vector2(Controller.rb2d.velocity.x, JUMP_HEIGHT);
        }
    }

    public void PerformFormChangeJump()
    {
        if(CanJump || !Character.SwapHandler.isTransformed)
        {
            return;
        }

        Controller.animator.SetTrigger("Jump");
        Controller.animator.SetBool("Grounded", false);
        Character.SwapHandler.ResetForm();
        Controller.rb2d.velocity = new Vector2(Controller.rb2d.velocity.x, JUMP_HEIGHT * 1.2f);
    }

    public override void UpdateAbility()
    {
        if (!Active)
        {
            return;
        }

        if (coyoteTimeCounter > 0f)
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (jumpTimer > 0f)
        {
            jumpTimer -= Time.deltaTime;
        }
        if(jumpHoldTimer > 0f)
        {
            jumpHoldTimer -= Time.deltaTime;
        }
        if (Controller.isOnGround)
        {
            ResetJumps();
        }
    }

    private void ResetJumps()
    {
        jumpsRemaining = NUM_JUMPS;
        jumpTimer = 0f;
        coyoteTimeCounter = COYOTE_TIME;
    }

    public void SetMonsterReference(MonsterCharacter character)
    {
        Debug.Log("Received!!!");
        Character = character;
        Controller = Character.Controller;
        Character.Abilities.Jump = this;
    }

    public void OnFormChange(FormInfo info)
    {
        MonsterFormAbility.Activate(this, info.hasJump);
        JUMP_HEIGHT = info.jumpHeight;
        NUM_JUMPS = info.numJumps;
        ejectsound.clip = audioClip2;
        ejectsound.Play ();
    }
    public AudioSource AddAudio(float vol) 
    { 
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.playOnAwake = false;
        newAudio.volume = vol; 
        return newAudio; 
     }

}

