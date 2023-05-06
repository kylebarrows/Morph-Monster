using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : IButtonPairable
{
    private BoxCollider2D boxCollider;
    [SerializeField] public Animator doorAnimator;

    [SerializeField] private float doorHeight;
    [SerializeField] private float openSpeed;

    private bool shouldMove;
    private bool opening;
    private bool closing;
    private bool isOpen;

    Vector3 startPos;
    Vector3 endPos;
    
    void Awake()
    {
        doorAnimator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        startPos = transform.position;
        endPos = transform.position + new Vector3(0.0f, 4.0f, 0.0f);
    }

    private void Update()
    {
        if (shouldMove)
        {
            if(!isOpen && opening)
            {
                OpenDoor();
            }
            else if (isOpen && closing)
            {
                CloseDoor();
            }
        }
        else if(isOpen && closing)
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        float dist = Vector3.Distance(transform.position, endPos);
        if (dist > .1f)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, endPos, openSpeed * Time.deltaTime);
            transform.position = newPos;
        }
        else
        {
            isOpen = true;
            opening = false;
            shouldMove = false;
        }
    }

    private void CloseDoor()
    {
        //doorAnimator.SetTrigger("close");
        float dist = Vector3.Distance(transform.position, startPos);
        if (dist > .1f)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, openSpeed * Time.deltaTime);
        }
        else
        {
            isOpen = false;
            closing = false;
            shouldMove = false;
        }
    }

    public override void OnButtonPressed()
    {
        shouldMove = true;
        opening = true;
    }

    public override void OnButtonReleased()
    {
        shouldMove = true;
        closing = true;
    }
   
}
