using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    private GameObject boss;
    private Health bossHealth;
    private Slider slider;


    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossHealth = boss.GetComponent<Health>();
        slider = GetComponent<Slider>();


    }

    void Update()
    {
        float fillValue = bossHealth.currentHealth / bossHealth.startingHealth;
        slider.value = fillValue;
    }
}
