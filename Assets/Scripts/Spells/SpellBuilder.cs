using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;

public class SpellBuilder 
{
    private JObject spell_attributes = new JObject();
    private List<string> spell_names = new List<string>();


    public Spell BuildSpell(string name, SpellCaster owner)
    {
        Spell spell = MakeSpell(name, owner);
        spell.SetAttributes((JObject)spell_attributes[name]);
        return spell;
    }

   
    public SpellBuilder()
    {        
        spell_attributes = ReadSpellsJson();
    }

    private Spell MakeSpell(string name, SpellCaster owner) {
        if (name == "") {
            return new MagicMissile(owner);
        }
        if (name == "arcane_blast") {
            return new ArcaneBlast(owner);
        }
        if (name == "arcane_railgun") {
            return new ArcaneRailgun(owner);
        }
        if (name == "arcane_spray") {
            return new ArcaneSpray(owner);
        }
        if (name == "arcane_bolt") {
            return new ArcaneBolt(owner);
        }
        if (name == "damage_amp") { 
            return new DamageAmp(owner);
        }
        if (name == "speed_amp") {
            return new SpeedAmp(owner);
        }
        if (name == "doubler") {// not made
            return new Doubler(owner);
        }
        if (name == "splitter") {// not made
            return new Splitter(owner);
        }
        if (name == "chaos") {// not made
            return new Chaotic(owner);
        }
        if (name == "homing") { // not made
            return new Homing(owner);
        }
        return null;

    }
    public Spell MakeRandomSpell(SpellCaster owner) {
        System.Random rnd = new System.Random();
        int index = rnd.Next(spell_names.Count);

        // create random spell
        Spell s = BuildSpell(spell_names[index], owner);
        // if spell is modifier then loop to create random spell again until we get a base spell
        if (s is ModifierSpell modifierSpell) {
            modifierSpell.AddChild(MakeRandomSpell(owner));
        }
        return s;
    }

    public JObject ReadSpellsJson()
    {
        var spelltext = Resources.Load<TextAsset>("spells");
        JObject spell_types = JObject.Parse(spelltext.text);
        foreach(var a in spell_types)
        {
            spell_names.Add(a.Key);
        }
        return spell_types;
    }
}

