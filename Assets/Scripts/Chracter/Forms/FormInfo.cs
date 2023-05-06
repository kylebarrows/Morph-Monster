using UnityEngine;

public abstract class FormInfo
{
    public FormNames Form;

    public int FormID;
    public Vector2 ColliderSize;
    public Vector2 ColliderOffset;

    public Vector2 scale;

    public float moveSpeed = 6;
    public float jumpHeight = 12;
    public float numJumps = 1;
    public float climbSpeed = 15;
    public int attackDamage = 0;
    public float attackRange = 0;

    public int bonusHealth = 3;

    public bool hasRun = false;
    public bool hasJump = false;
    public bool hasClimb = false;
    public bool hasConsume = false;
    public bool hasPushPull = false;
    public bool hasAttack = false;
    public bool hasAimAndFire = false;

}
