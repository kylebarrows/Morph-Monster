using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterClamber : MonsterFormAbility, IMonsterReceiver
{
    MonsterCharacter Character;

    MonsterController Controller;

    [SerializeField] private float CLAMBER_SPEED;

    MovementHandler movementHandler => Controller.MovementHandler;

    public bool isClambering;

    public override void UpdateAbility()
    {
        if (isClambering)
        {
            if(!movementHandler.isInPlatform)
            {
                isClambering = false;
            }
        }

        if (movementHandler.isInPlatform)
        {
            if(movementHandler.isFalling)
            {
                return;
            }
            PerformPlatformClamber();
        }
    }

    private void PerformPlatformClamber()
    {
        isClambering = true;
        Vector2 currentVel = Controller.rb2d.velocity;
        Vector2 newVel = new Vector2(currentVel.x, Mathf.Min(currentVel.y + CLAMBER_SPEED, 12 ));
        Controller.rb2d.velocity = newVel;
    }

    public void SetMonsterReference(MonsterCharacter character)
    {
        Character = character;
        Controller = Character.Controller;
        Character.Abilities.Clamber = this;
    }
}
