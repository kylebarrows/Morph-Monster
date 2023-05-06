using UnityEngine;
public class MeleeFormInfo : FormInfo
{
    public MeleeFormInfo()
    {
        Form = FormNames.Melee;

        FormID = 1;

        ColliderSize = new Vector2(0.59f, 1.73f);
        ColliderOffset = new Vector2(-0.05f, -0.03f);
        scale = new Vector2(1f, 1f);

        moveSpeed = 6;
        jumpHeight = 12;
        numJumps = 1;
        climbSpeed = 20;
        bonusHealth = 3;
        attackDamage = 1;
        attackRange = 0.5f;

        hasRun = true;
        hasJump = true;
        hasClimb = true;
        hasPushPull = false;
        hasConsume = false;
        hasAttack = true;
    }
}
