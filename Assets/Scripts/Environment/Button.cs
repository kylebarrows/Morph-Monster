using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    
    [SerializeField] private IButtonPairable otherObject;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private bool staysActive;
    [SerializeField] private float buttonReleaseDelay;
    private float buttonReleaseTimer;

    private bool buttonPressed;
    private bool shouldReleaseButton;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MonsterController mc = collision.gameObject.GetComponent<MonsterController>();
        if(mc)
        {
            ActivateButton();
            return;
        }

        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if(projectile)
        {
            ActivateButton();
            return;
        }

        buttonPressed = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(staysActive)
        {
            return;
        }

        MonsterController mc = collision.gameObject.GetComponent<MonsterController>();
        if (mc)
        {
            shouldReleaseButton = true;
            buttonReleaseTimer = buttonReleaseDelay;
            return;
        }

        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile)
        {
            shouldReleaseButton = true;
            buttonReleaseTimer = buttonReleaseDelay;
            return;
        }

    }

    private void ActivateButton()
    {
        spriteRenderer.color = Color.green;
        buttonPressed = true;
        shouldReleaseButton = false;
        otherObject.OnButtonPressed();
    }

    // Update is called once per frame
    void Update()
    {
        if(!staysActive && buttonReleaseTimer <= 0)
        {
            if(shouldReleaseButton)
            {
                shouldReleaseButton = false;
                otherObject.OnButtonReleased();
                spriteRenderer.color = Color.red;
            }
        }

        if(buttonReleaseTimer > 0)
        {
            buttonReleaseTimer -= Time.deltaTime;
        }
    }
}
