using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterClimb : MonsterFormAbility, IMonsterReceiver
{
    private MonsterCharacter Character;

    private MonsterController Controller;

    private ClimbableBehavior climbable;

    [SerializeField] public float CLIMB_SPEED;

    public bool isClimbing = false;

    void Start()
    {
        FormSwapHandler.OnFormChange += OnFormChange;
    }
    public void SetMonsterReference(MonsterCharacter character)
    {
        Debug.Log("Received!!!");
        Character = character;
        Controller = Character.Controller;
        Character.Abilities.Climb = this;
    }

    public bool CanClimb()
    {
        if(Controller.MovementHandler.isInClimbZone)
        {
            return true;
        }

        return false;
    }

    public void Climb()
    {
        if(!Active)
        {
            return;
        }

        climbable = Controller.MovementHandler.Climbable;
        if (climbable)
        {
            isClimbing = true;
            transform.position = new Vector2(climbable.GetCenterX(), Controller.transform.position.y);
            Controller.rb2d.velocity = Vector2.zero;
            if (transform.position.y <= climbable.GetTopBounds().y && Controller.transform.position.y >= climbable.GetLowerBounds().y)
            {
                float offset = (CLIMB_SPEED * Controller.MInput.verticalDir * Time.deltaTime) + Controller.transform.position.y;
                Vector2 newPosition = new Vector2(transform.position.x, offset);
                Controller.rb2d.position = newPosition;
            }
        }
        else
        {
            StopClimbing();
        }
    }

    public void StopClimbing()
    {
        climbable = null;
        isClimbing = false;
    }

    public void OnFormChange(FormInfo info)
    {
        MonsterFormAbility.Activate(this, info.hasClimb);
        CLIMB_SPEED = info.climbSpeed;
    }
}
