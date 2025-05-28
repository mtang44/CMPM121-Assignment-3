using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine;

public class Recoiling : ModifierSpell
{
    private string damage_multiplier;

    public Recoiling (SpellCaster owner) : base(owner)
    {

    }

    public override void SetAttributes(JObject attributes)
    {
        base.SetAttributes(attributes);
        damage_multiplier = attributes["damage_multiplier"].ToString();
    }

    public override ValueModifier AddMods(ValueModifier mods)
    {
        //ValueModifier damageModifier = new ValueModifier(damage_multiplier, true);
        //mods.damageMods.Add(damageModifier);
        //return inner.GetDamage(mods);
        mods.AddMod("damage_mult", damage_multiplier);
        return mods;
    }

    public override IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team, ValueModifier mods)
    {
        //ValueModifier damageModifier = new ValueModifier(damage_multiplier, true);
        //mods.damageMods.Add(damageModifier);
        ValueModifier new_mods = AddMods(mods);
        GameManager.Instance.player.GetComponent<Unit>().Move((Vector2)(where - target).normalized * 1); // change the # being multiplied to change amount recoiled
        CoroutineManager.Instance.Run(this.child.Cast(where, target, team, new_mods));
        yield return new WaitForEndOfFrame();
    }
}
