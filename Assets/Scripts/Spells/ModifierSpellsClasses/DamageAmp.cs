using System.Collections.Generic;
public class DamageAmp : ModifierSpell {

private string damage_multiplier;
        private string mana_multiplier;
    
    public DamageAmp () {
    
        
    }

    public void override SetAttributes(JObject attributes) {
        base.SetAttributes();
        damage_multiplier = attributes["damage_multiplier"].ToString();
        mana_multiplier = aattributes["mana_multiplier"].ToString();
      
    }
    public bool override IsModifierSpell()
    {
        return true;
    }
    public void override Cast()
    {
        // will call override of base spell to implement modifier's attributes. 
    }
}