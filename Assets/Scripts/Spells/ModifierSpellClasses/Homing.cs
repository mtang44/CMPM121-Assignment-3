using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

public class Homing : ModifierSpell {

    private string mana_adder;
    private string damage_multiplier;
    private string projectile_trajectory;
    public Homing (SpellCaster owner) : base(owner) {
        
        
    }

    public override void SetAttributes(JObject attributes) {
        base.SetAttributes(attributes);
        damage_multiplier = attributes["damage_multiplier"].ToString();
        mana_adder = attributes["mana_adder"].ToString();
        projectile_trajectory = attributes["projectile_trajectory"].ToString();
      
    }
    
    public override ValueModifier AddMods (ValueModifier mods) {
        
        return mods;
    }
}