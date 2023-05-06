using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public Transform target; 
    public float minimumDistance;

    public void Update()
    {
        // if ()
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
         
    }

}
