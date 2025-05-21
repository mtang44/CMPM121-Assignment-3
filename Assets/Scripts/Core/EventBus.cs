using UnityEngine;
using System;

public class EventBus
{
    private static EventBus theInstance;
    public static EventBus Instance
    {
        get
        {
            if (theInstance == null)
                theInstance = new EventBus();
            return theInstance;
        }
    }

    public event Action<Vector3, Damage, Hittable> OnDamage;

    public void DoDamage(Vector3 where, Damage dmg, Hittable target)
    {
        OnDamage?.Invoke(where, dmg, target);
    }

    public event Action<Vector3, Hittable> OnMove;

    public void DoMove(Vector3 where, Hittable who)
    {
        OnMove?.Invoke(where, who);
    }

    public event Action<Vector3, Hittable> OnKill;

    public void DoKill(Vector3 where, Hittable target)
    {
        OnKill?.Invoke(where, target);
    }

    public event Action<SpellCaster, Spell> OnEndCast;

    public void DoEndCast(SpellCaster who, Spell spell)
    {
        OnEndCast?.Invoke(who, spell);
    }
    public event Action<Relic> OnRelicPickup;

    public void DoRelicPickup(Relic relic)
    {
        OnRelicPickup?.Invoke(relic);
    }

}
