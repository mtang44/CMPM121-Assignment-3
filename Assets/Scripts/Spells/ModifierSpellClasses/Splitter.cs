using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

public class Splitter : ModifierSpell {
    private string mana_multiplier;
    private string angle;
    public Splitter (SpellCaster owner) : base(owner) {
        
        
    }

    public override void SetAttributes(JObject attributes) {
        base.SetAttributes(attributes);
        angle = attributes["angle"].ToString();
        mana_multiplier = attributes["mana_multiplier"].ToString();
      
    }
    
    public override ValueModifier AddMods (ValueModifier mods) {
        mods.AddMod("mana_cost_mult", mana_multiplier);
        return mods;
    }

    public override IEnumerator Cast (Vector3 where, Vector3 target, Hittable.Team team, ValueModifier current_mods) {
        Vector3 direction = target - where;
        float angle = Mathf.Atan2(direction.y, direction.x);
        float split1 = Random.value * GetRPNFloat(this.angle) * Mathf.Deg2Rad;
        float split2 = Random.value * GetRPNFloat(this.angle) * Mathf.Deg2Rad;
        CoroutineManager.Instance.Run(this.child.Cast(where, where + new Vector3(Mathf.Cos(angle + split1), Mathf.Sin(angle + split1), 0), team, AddMods(current_mods)));
        CoroutineManager.Instance.Run(this.child.Cast(where, where + new Vector3(Mathf.Cos(angle - split2), Mathf.Sin(angle - split2), 0), team, AddMods(current_mods)));
        yield return new WaitForEndOfFrame();
    }
}