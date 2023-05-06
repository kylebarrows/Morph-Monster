using UnityEngine;
public class MonsterFormInfo : FormInfo
{
    public MonsterFormInfo()
    {
        FormID = 0;

        ColliderSize = new Vector2(1.02f, 1.8f);
        ColliderOffset = new Vector2(-0.14f, 0f);
        scale = new Vector2(0.8f, 0.87f);

        moveSpeed = 6f;
        jumpHeight = 12f;
        numJumps = 1f;
        climbSpeed = 0f;
        bonusHealth = 1;
        attackRange = 0;
        attackDamage = 0;

        hasRun = true;
        hasJump = true;
        hasClimb = false;
        hasPushPull = false;
        hasConsume = true;
        hasAttack = false;
    }
}
