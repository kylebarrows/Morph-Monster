using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableObject : MonoBehaviour
{
    private static float acceptableDistanceFromConsumer = 1.0f;

    public bool isConsumed;

    public bool isBeingConsumed;

    [SerializeField] public int FormID;
    //public bool isConsumeTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MoveTowardsConsumer(MonsterController target)
    {
        Vector2 targetLocation = target.transform.position;
        Vector2 startLocation = new Vector2(transform.position.x, transform.position.y);
        if (Vector2.Distance(startLocation, targetLocation) < acceptableDistanceFromConsumer)
        {
            isConsumed = true;
        }
        else
        {
            Vector2 iPosition = Vector2.Lerp(startLocation, targetLocation, 0.06f);
            transform.position = new Vector3((float)iPosition.x, (float)iPosition.y, 0f);
        }
    }

    public void HandleConsumption()
    {
        isConsumed = true;
        Health health = gameObject.GetComponent<Health>();
        if(health)
        {
            health.HandleConsume();
        }
        else
        {
            // it's a corpse or other object
            Destroy(gameObject);
        }
    }
}
