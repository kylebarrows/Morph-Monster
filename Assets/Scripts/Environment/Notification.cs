using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
   public GameObject uiObject;
    void Start()
    {
        uiObject.SetActive(false);
    }
    void OnTriggerEnter2D (Collider2D collision)
    {
        MonsterController mc = collision.gameObject.GetComponent<MonsterController>();
        if (mc)
        {
            uiObject.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        uiObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
