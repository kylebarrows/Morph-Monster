// Base class for all forms
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class FormBase : MonoBehaviour
{
    public FormNames FormName;

    public MonsterCharacter Character;
    
    public SpriteRenderer SpriteRenderer;

    public Animator Animator;

    public Rigidbody2D rb2d;

    private BoxCollider2D collider;

    public FormMovementInfo MovementInfo;

    public virtual void OnEnter()
    { 
    }

    public virtual void OnExit()
    {
    }
}
