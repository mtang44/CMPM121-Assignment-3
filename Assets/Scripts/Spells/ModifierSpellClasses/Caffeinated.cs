using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

public class Caffeinated : ModifierSpell {

private string damage_multiplier;
private string speed_multiplier;
private string mana_multiplier;
    
    public Caffeinated (SpellCaster owner) : base(owner) {
    
    }

    public override void SetAttributes(JObject attributes) {
        base.SetAttributes(attributes);
        damage_multiplier = attributes["damage_multiplier"].ToString();
        speed_multiplier = attributes["speed_multiplier"].ToString();
        mana_multiplier = attributes["mana_multiplier"].ToString();

    }

    public override ValueModifier AddMods (ValueModifier mods) {
        mods.AddMod("damage_mult", damage_multiplier);
        mods.AddMod("speed_mult", speed_multiplier);
        mods.AddMod("mana_cost_mult", mana_multiplier);
        return mods;
    }

}