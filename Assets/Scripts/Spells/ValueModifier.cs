using System.Collections.Generic;
public class ValueModifier {
    public Dictionary<string, List<string>> modifiers = new Dictionary<string, List<string>>();
    /* 
    Keys:

        Stat Mods (increment / multiply a number):
        mana_cost_add
        mana_cost_mult
        damage_add
        damage_mult
        delay_mult
        cooldown_mult
        speed_mult
        angle_add

        Behavior Mods (mods that alter projectile):
        trajectory

    
    */
    public ValueModifier() {

    }

    public void AddMod (string key, string modifier) {
        if (modifiers[key] == null) {
            modifiers[key] = new List<string>();
        }
        modifiers[key].Add(modifier);
    }

}
