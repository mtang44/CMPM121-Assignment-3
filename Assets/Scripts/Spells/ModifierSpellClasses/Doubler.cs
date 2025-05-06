using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

public class Doubler : ModifierSpell {

    private string delay;
    private string mana_multiplier;
    private string cooldown_multiplier;

    public Doubler (SpellCaster owner) : base(owner) {       
        
    }

    public override void SetAttributes(JObject attributes) {
        base.SetAttributes(attributes);
        delay = attributes["delay"].ToString();
        cooldown_multiplier = attributes["cooldown_multiplier"].ToString();
        mana_multiplier = attributes["mana_multiplier"].ToString();
      
    }
    
    public override ValueModifier AddMods (ValueModifier mods) {
        mods.AddMod("cooldown_mult", cooldown_multiplier);
        mods.AddMod("mana_cost_mult", mana_multiplier);
        return mods;
    }

    public override IEnumerator Cast (Vector3 where, Vector3 target, Hittable.Team team, ValueModifier current_mods) {

        CoroutineManager.Instance.Run(this.child.Cast(where, target, team, AddMods(current_mods)));
        yield return new WaitForSeconds(GetRPNFloat(delay));
        CoroutineManager.Instance.Run(this.child.Cast(where, target, team, AddMods(current_mods)));
        yield return new WaitForEndOfFrame();
        // this.team = team;
        // GameManager.Instance.projectileManager.CreateProjectile(0, "straight", where, target - where, 15f, OnHit);
        // yield return new WaitForEndOfFrame();
    }
}