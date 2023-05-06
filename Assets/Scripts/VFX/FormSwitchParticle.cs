using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormSwitchParticle : MonoBehaviour
{
    public ParticleSystem particleSystem;

    public GameObject corpse;

    List<ParticleCollisionEvent> collisionEvents;

    private bool hasEmitted;

    void Start()
    {
       collisionEvents = new List<ParticleCollisionEvent>(); 
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleSystem, other, collisionEvents);
        if(!hasEmitted)
        {
            SpawnCorpse(collisionEvents[0].intersection);
        }
        hasEmitted = true;
        Destroy(gameObject);
    }

    private void SpawnCorpse(Vector3 position)
    {
        //Instantiate(corpse, new Vector2(position.x, position.y), Quaternion.identity);  
    }

    // Update is called once per frame
    void Update()
    {
        //particleSystem.Emit(1);
    }
}
