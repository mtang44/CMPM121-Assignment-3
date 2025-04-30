using System.Collections.Generic;
public class Homing : ModifierSpell {

    private string mana_adder;
    private string damage_multiplier;
    private string projectile_trajectory;
    public Homing () {
        
        
    }

    public void override SetAttributes(JObject attributes) {
        base.SetAttributes();
        damage_multiplier = attributes["damage_multiplier"].ToString();
        mana_adder = aattributes["mana_adder"].ToString();
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