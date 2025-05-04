using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

public class Splitter : ModifierSpell {

    private string delay;
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
        
        return mods;
    }
}