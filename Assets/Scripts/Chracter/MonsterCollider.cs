using UnityEngine;

public class MonsterCollider : MonoBehaviour
{
    private MonsterController monsterCtrl;

    private GameObject damagingObject;

    private float damageDealt;
    void Start()
    {
        monsterCtrl = MonsterController.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckForDamage(collision);
        CheckForRespawnPoint(collision);
    }

    private void CheckForRespawnPoint(Collider2D collision)
    {
        RespawnPost respawnPost = collision.GetComponent<RespawnPost>();
        if(respawnPost != null)
        {
            monsterCtrl.SaveState();
            monsterCtrl.respawnPost = respawnPost;
        }
    }

    private void CheckForDamage(Collider2D collision)
    {
        Hazard hazard = collision.gameObject.GetComponent<Hazard>();
        if (hazard != null)
        {
            damageDealt = hazard.damageDealt;
            damagingObject = collision.gameObject;
            monsterCtrl.TakeDamage(damagingObject, damageDealt);
            if(hazard.respawnPlayer)
            {
                monsterCtrl.RespawnAtLastPost();
            }
        }

        Fireball fireball = collision.gameObject.GetComponent<Fireball>();
        if(fireball != null)
        {
            damageDealt = fireball.damage;
            damagingObject = fireball.gameObject;
            monsterCtrl.TakeDamage(damagingObject, damageDealt);
            Destroy(fireball.gameObject);
        }
    }
}
