using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonsterFormAbility, IMonsterReceiver
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 1;

    private MonsterCharacter Character;

    private MonsterController Controller;
    // Start is called before the first frame update
    void Start()
    {
        FormSwapHandler.OnFormChange += OnFormChange;
    }

    public void Attack()
    {
        if(!Active)
        {
            return;
        }

        Controller.animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Health>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public override void UpdateAbility()
    {
    }

    public void OnFormChange(FormInfo info)
    {
        MonsterFormAbility.Activate(this, info.hasAttack);
        attackRange = info.attackRange;
        attackDamage = info.attackDamage;
    }

    public void SetMonsterReference(MonsterCharacter character)
    {
        Debug.Log("Received!!!");
        Character = character;
        Controller = Character.Controller;
        Character.Abilities.Attack = this;
    }
}
