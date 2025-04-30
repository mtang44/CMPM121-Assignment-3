using System.Collections.Generic;
public class Chaos : ModifierSpell {

    private string delay;
    private string projectile_trajectory;
    private string damage_multiplier;
    public Chaos () {
        
        
    }

    public void override SetAttributes(JObject attributes) {
        base.SetAttributes();
        damage_multiplier = attributes["damage_multiplier"].ToString();
        projectile_trajectory = attributes["projectile_trajectory"].ToString();
      
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