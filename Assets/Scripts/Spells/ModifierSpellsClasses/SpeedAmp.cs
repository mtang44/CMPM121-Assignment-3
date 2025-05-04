using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

public class SpeedAmp : ModifierSpell {

    private string speed_multiplier;
    public SpeedAmp (SpellCaster owner) : base(owner) {
        
        
    }

    public override void SetAttributes(JObject attributes) {
        base.SetAttributes(attributes);
        speed_multiplier = attributes["speed_multiplier"].ToString();
      
    }
    
    public override ValueModifier AddMods (ValueModifier mods) {
        
        return mods;
    }
}