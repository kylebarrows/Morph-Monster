using System;
using System.Reflection;
using UnityEngine;

[Serializable]
public class FormAbilities
{
    public MonsterRun Run;

    public MonsterJump Jump;

    public MonsterClimb Climb;

    public MonsterConsume Consume;

    public MonsterPushPull PushPull;

    public MonsterClamber Clamber;

    public MonsterAttack Attack;

    public MonsterShoot Shoot;

    public void DisableAllAbilities()
    {
        MonsterFormAbility.Deactivate(Run);
        MonsterFormAbility.Deactivate(Jump);
        MonsterFormAbility.Deactivate(Climb);
        MonsterFormAbility.Deactivate(Consume);
    }

    public MonsterFormAbility GetAbility(string stateName)
    {
        FieldInfo field = GetType().GetField(stateName);
        if (field != null)
        { 
            return (MonsterFormAbility)field.GetValue(MonsterCharacter.instance.Abilities);
        }
        Debug.LogError("MonsterAbilities: Could not find Ability named " + stateName + " in Ability");
        return null;
    }
}
