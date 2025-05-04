using System.Collections.Generic;
public class Doubler : ModifierSpell {

    private string delay;
    private string mana_multiplier;
    private string cooldown_multiplier;
    public Doubler () {
        
        
    }

    public void override SetAttributes(JObject attributes) {
        base.SetAttributes();
        delay = attributes["delay"].ToString();
        cooldown_multiplier = attributes["cooldown_multiplier"].ToString();
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