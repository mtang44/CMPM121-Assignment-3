using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

public class Piercing : ModifierSpell {

    private string pierce_adder;

    public Piercing (SpellCaster owner) : base(owner) {       
        
    }

    public override void SetAttributes(JObject attributes) {
        base.SetAttributes(attributes);
        this.pierce_adder = attributes["pierce_adder"].ToString();
      
    }
    
    public override ValueModifier AddMods (ValueModifier mods) {
        mods.AddMod("pierce_add", pierce_adder);
        return mods;
    }

}