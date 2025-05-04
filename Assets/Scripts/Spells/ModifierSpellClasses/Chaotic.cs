using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

public class Chaotic : ModifierSpell {
    private string delay;
    private string projectile_trajectory;
    private string damage_multiplier;
    public Chaotic (SpellCaster owner) : base(owner) {
        
        
    }

    public override void SetAttributes(JObject attributes) {
        base.SetAttributes(attributes);
        damage_multiplier = attributes["damage_multiplier"].ToString();
        projectile_trajectory = attributes["projectile_trajectory"].ToString();
      
    }

    public override ValueModifier AddMods (ValueModifier mods) {
        
        return mods;
    }
}