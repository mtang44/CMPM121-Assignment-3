using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

public class DamageAmp : ModifierSpell {

private string damage_multiplier;
private string mana_multiplier;
    
    public DamageAmp (SpellCaster owner) : base(owner) {
    
    }

    public override void SetAttributes(JObject attributes) {
        base.SetAttributes(attributes);
        damage_multiplier = attributes["damage_multiplier"].ToString();
        mana_multiplier = attributes["mana_multiplier"].ToString();
    }

    public override ValueModifier AddMods (ValueModifier mods) {
        mods.AddMod("damage_mult", damage_multiplier);
        mods.AddMod("mana_cost_mult", mana_multiplier);
        return mods;
    }

}