using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbableBehavior : MonoBehaviour
{
    private BoxCollider2D collider;
    private Vector2 upTo;
    private Vector2 downTo;
    
    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        upTo = new Vector2(collider.bounds.center.x, collider.bounds.max.y);
        downTo = new Vector2(collider.bounds.center.x, collider.bounds.min.y);
    }

    public Vector2 GetTopBounds()
    {
        return upTo;
    }

    public Vector2 GetLowerBounds()
    {
        return downTo;
    }
    public float GetCenterX()
    {
        return collider.bounds.center.x;
    }
}
