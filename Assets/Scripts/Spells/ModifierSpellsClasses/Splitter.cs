using System.Collections.Generic;
public class Splitter : ModifierSpell {

    private string delay;
    private string mana_multiplier;
    private string angle;
    public Splitter () {
        
        
    }

    public void override SetAttributes(JObject attributes) {
        base.SetAttributes();
        angle = attributes["angle"].ToString();
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