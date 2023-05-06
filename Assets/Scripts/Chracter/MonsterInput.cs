using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterInput : MonoBehaviour
{
    public MonsterController Controller;
    private InputSystem inputSystem;

    public float horizontalDir;
    public float verticalDir;

    public bool jumpPressed;
    public bool jumpHeld;
    public bool jumpReleased;

    public bool downJumpPressed;

    public bool consumeHeld;

    public bool ejectFormPressed;

    public bool pullObjectHeld;

    public bool aimHeld;

    public bool shootPressed;

    public bool attackHeld;

    private void Awake()
    {
        inputSystem = new InputSystem();
    }

    private void OnEnable()
    {
        inputSystem.Enable();
    }

    private void OnDisable()
    {
        inputSystem.Disable();
    }

    void Start()
    {
        Controller = MonsterController.instance;
        jumpPressed = false;
        jumpHeld = false;
        downJumpPressed = false;
    }

    private void OnMove(InputValue value)
    {
        Vector2 dir = value.Get<Vector2>();
        horizontalDir = dir.x;
        verticalDir = dir.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Jump inputs
        jumpPressed = inputSystem.Monster.Jump.ReadValue<float>() == 1 ? true : false;
        jumpHeld = jumpPressed;
        jumpReleased = !jumpPressed;
        downJumpPressed = jumpHeld && verticalDir < -0.6f;

        // Consume & attack
        consumeHeld = inputSystem.Monster.Consume.ReadValue<float>() == 1 ? true : false;
        attackHeld = Input.GetMouseButton(0);

        // Aim & Fire
        aimHeld = inputSystem.Monster.Aim.ReadValue<float>() >= 0.6 ? true : false;
        shootPressed = inputSystem.Monster.Shoot.ReadValue<float>() >= 0.6 ? true : false;

        // Form Swap
        ejectFormPressed = inputSystem.Monster.Swap.ReadValue<float>() == 1 ? true : false;

        // push pull
        pullObjectHeld = inputSystem.Monster.PushPull.ReadValue<float>() == 1 ? true : false;

    }
}
