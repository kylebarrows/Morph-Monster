using System;
using UnityEngine;

public abstract class MonsterFormAbility : MonoBehaviour
{
    public bool Active;

    public static void Activate(MonsterFormAbility ability)
    {
        Activate(ability, active: true);
    }

    public static bool IsActive(MonsterFormAbility ability)
    {
        return ability.Active;
    }

    public static void UpdateAbility(MonsterFormAbility ability)
    {
        if (ability != null && ability.Active)
        {
            ability.UpdateAbility();
        }
    }

    public static void Activate(MonsterFormAbility ability, bool active)
    {
        ability.Active = active;
    }

    public static void Deactivate(MonsterFormAbility ability)
    {
        Activate(ability, active: false);
    }

    public virtual void UpdateAbility()
    {
    }
}
