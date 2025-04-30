using System.Collections.Generic;
public class SpeedAmp : ModifierSpell {

    private string speed_multiplier;
    public SpeedAmp () {
        
        
    }

    public void override SetAttributes(JObject attributes) {
        base.SetAttributes();
        speed_multiplier = attributes["speed_multiplier"].ToString();
      
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