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
        
        return mods;
    }
}