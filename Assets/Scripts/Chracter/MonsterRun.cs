using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRun : MonsterFormAbility, IMonsterReceiver
{
    public MonsterCharacter Character;

    public MonsterController Controller;

    [SerializeField] public float MOVE_SPEED;

    public bool isFacingRight;

    void Start()
    {
        isFacingRight = true;
        FormSwapHandler.OnFormChange += OnFormChange;
    }

    private void FaceRight()
    {
        Controller.facingRight = true;
        Vector3 newScale = transform.localScale;
        newScale.x = newScale.x < 0 ? -1 * newScale.x : newScale.x;
        transform.localScale = newScale;
    }

    private void FaceLeft()
    {
        Controller.facingRight = false;
        Vector3 newScale = transform.localScale;
        newScale.x = -1f * newScale.x;
        transform.localScale = newScale;
    }

    public void Move()
    {
        if (!Active)
        {
            return;
        }
        
        if(Character.Abilities.Shoot.isAiming)
        {
            return;
        }

        if (Character.MInput.horizontalDir != 0f)
        {
            Controller.animator.SetInteger("AnimState", 1);
            if (Controller.MInput.horizontalDir > 0)
            {
                if (!Controller.facingRight)
                {
                    FaceRight();
                }
            }
            if (Controller.MInput.horizontalDir < 0)
            {
                if (Controller.facingRight)
                {
                    FaceLeft();
                }
            }
        }
        else
        {
            Controller.animator.SetInteger("AnimState", 0);
        }
        
        float xVelocity = Controller.MovementHandler.movingPlatformVelocity.x + (Controller.MInput.horizontalDir * MOVE_SPEED);
        float yVelocity = Controller.MovementHandler.movingPlatformVelocity.y + Controller.rb2d.velocity.y;
        Vector2 newVelocity = new Vector2(xVelocity, yVelocity);
        Controller.rb2d.velocity = newVelocity;
    }

    public void StopRunning()
    {
        Controller.rb2d.velocity = new Vector2(0f, Controller.rb2d.velocity.y);
    }

    public void OnFormChange(FormInfo info)
    {
        MonsterFormAbility.Activate(this, info.hasRun);
        MOVE_SPEED = info.moveSpeed;
    }

    public void SetMonsterReference(MonsterCharacter character)
    {
        Debug.Log("Received!!!");
        Character = character;
        Character.Abilities.Run = this;
        Controller = Character.Controller;
    }
    

    public override void UpdateAbility()
    { 
    }
}
