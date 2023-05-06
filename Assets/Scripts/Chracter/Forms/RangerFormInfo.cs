using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerFormInfo : FormInfo
{
    public RangerFormInfo()
    {
        Form = FormNames.Ranger;

        FormID = 3;

        ColliderSize = new Vector2(4.19f, 9.09f);
        ColliderOffset = new Vector2(-1.14f, -0.31f);
        scale = new Vector2(0.18f, 0.18f);

        moveSpeed = 5;
        jumpHeight = 10;
        numJumps = 1;
        climbSpeed = 20;
        bonusHealth = 3;
        attackDamage = 2;
        attackRange = 0.75f;

        hasRun = true;
        hasJump = true;
        hasClimb = true;
        hasAttack = false;
        hasAimAndFire = true;
    }
}