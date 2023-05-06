using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteFormInfo : FormInfo
{
    public BruteFormInfo()
    {
        Form = FormNames.Brute;

        FormID = 2;

        ColliderSize = new Vector2(.7f, .7f);
        ColliderOffset = new Vector2(0f, 0f);
        scale = new Vector2(4f, 4f);

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
        hasPushPull = true;
        hasConsume = false;
        hasAttack = true;
    }
}
