using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterConsume : MonsterFormAbility, IMonsterReceiver
{
    MonsterCharacter Character;

    MonsterController Controller;

    Vector2 endLocation;

    public bool isConsuming;

    public bool targetConsumed;

    [SerializeField] private float CONSUME_RANGE;
    [SerializeField] private float CONSUME_SPHERE_RADIUS;
    [SerializeField] private LayerMask CONSUME_LAYER;
    [SerializeField] private LayerMask PROJECTILE_LAYER;
    [SerializeField] private GameObject ConsumeProjectilePrefab;
    [SerializeField] private bool consumedProjectile;

    public void Start()
    {
        consumedProjectile = false;
        FormSwapHandler.OnFormChange += OnFormChange;
    }

    public void StartConsume()
    {
        if(!Active)
        {
            return;
        }

        if(Character.SwapHandler.isTransformed)
        {
            Controller.animator.SetBool("ConsumeHeld", false);
            return; 
        }

        Controller.animator.SetBool("ConsumeHeld", true);
        CheckForConsumeInArea();
    }

    public void CheckForConsumeInArea()
    {
        if(targetConsumed || Character.SwapHandler.isTransformed)
        {
            return;
        }

        Vector2 startLocation = Controller.collider.bounds.center;
        float xOffset = CONSUME_RANGE;

        if(!Controller.facingRight)
        {
            xOffset = -1 * CONSUME_RANGE;
        }

        endLocation = new Vector2(startLocation.x + xOffset, startLocation.y);
        Collider2D[] enemiesToConsume = Physics2D.OverlapCircleAll(endLocation, CONSUME_SPHERE_RADIUS, CONSUME_LAYER);
        foreach (Collider2D enemy in enemiesToConsume)
        {
            ConsumableObject cObj = enemy.gameObject.GetComponent<ConsumableObject>();
            if(cObj != null)
            {
                cObj.MoveTowardsConsumer(Controller);
                if(cObj.isConsumed)
                {
                    HandleEnemyConsumption(cObj);
                }
            }
        }

        Collider2D[] projectilesToConsume = Physics2D.OverlapCircleAll(endLocation, CONSUME_SPHERE_RADIUS, PROJECTILE_LAYER);
        foreach (Collider2D projectile in projectilesToConsume)
        {
            Fireball fireball = projectile.gameObject.GetComponent<Fireball>();
            if (fireball != null)
            {
                if (Vector3.Distance(projectile.transform.position, transform.position) < 3.0f)
                {
                    HandleProjectileConsumption(fireball);
                }
            }
        }
    }

    public void HandleEnemyConsumption(ConsumableObject consumable)
    {
        targetConsumed = true;
        Character.ChangeForm(consumable.FormID, false);
        consumable.HandleConsumption();
    }

    public void HandleProjectileConsumption(Fireball projectile)
    {
        consumedProjectile = true;
        Destroy(projectile.gameObject);

    }

    private void ShootConsumedProjectile()
    {
        if (ConsumeProjectilePrefab)
        {
            Vector3 direction = new Vector3(transform.localScale.x, 0.0f, 0.0f);
            Vector3 projectileSpawn = transform.position + direction;
            GameObject projectileGO = Instantiate(ConsumeProjectilePrefab, projectileSpawn, Quaternion.identity);
            Projectile projectile = projectileGO.GetComponent<Projectile>();

            if (projectile)
            {
                projectile.SpawnProjectile(Controller.collider, direction, 1.0f);
            }
        }

        consumedProjectile = false;
    }

    public void OnFormChange(FormInfo info)
    {
        targetConsumed = false;
        consumedProjectile = false;
        MonsterFormAbility.Activate(this, info.hasConsume);
    }

    public override void UpdateAbility()
    {
        if (!Active || Character.SwapHandler.isTransformed)
        {
            return;
        }

        if(Character.MInput.consumeHeld)
        {
            if(Character.MInput.aimHeld && consumedProjectile)
            {
                ShootConsumedProjectile();
            }
            else
            {
                Controller.animator.SetBool("ConsumeHeld", true);
                CheckForConsumeInArea();
            }
        }
        else
        {
            Controller.animator.SetBool("ConsumeHeld", false);
        }
    }

    public void SetMonsterReference(MonsterCharacter character)
    {
        Character = character;
        Controller = Character.Controller;
        Character.Abilities.Consume = this;
    }
}
