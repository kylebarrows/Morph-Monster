using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void Update()
    {
        if(health.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
