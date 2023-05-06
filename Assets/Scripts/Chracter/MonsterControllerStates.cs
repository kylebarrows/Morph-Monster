using System;
using System.Reflection;
using UnityEngine;

public class MonsterControllerStates
{
    public bool facingRight;

    public bool isGrounded;

    public bool isJumping;

    public bool isFalling;

    public bool isClimbing;

    public bool inClimbZone;

    public bool onPlatform;

    public bool isHanging;

    public bool jumpingOffLedge;
    public MonsterControllerStates()
    {
        facingRight = false;
        Reset();
    }

    public bool GetState(string stateName)
    {
        FieldInfo field = GetType().GetField(stateName);
        if(field != null)
        {
            return (bool)field.GetValue(MonsterController.instance); ;
        }
        Debug.LogError("MonsterControllerStates: Could not find state named " + stateName + " in states");
        return false;
    }

    public void Reset()
    {
        isGrounded = false;
        isJumping = false;
        isFalling = false;
        inClimbZone = false;
    }
}
