using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;

public class SpellBuilder 
{
    private JObject spell_attributes;
    private List <string> spell_names;


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
        if (name == "Arcane Blast") {
            return new ArcaneBlast(owner);
        }
        if (name == "Arcane Railgun") {
            return new ArcaneRailgun(owner);
        }
        if (name == "Arcane Spray") {
            return new ArcaneSpray(owner);
        }
        if (name == "Arcane Bolt") {
            return new ArcaneBolt(owner);
        }
        if (name == "damage-amplified") { 
            return new DamageAmp(owner);
        }
        if (name == "speed-amplified") {
            return new SpeedAmp(owner);
        }
        if (name == "doubled") {// not made
            return new Doubler(owner);
        }
        if (name == "split") {// not made
            return new Splitter(owner);
        }
        if (name == "chaotic") {// not made
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
        foreach(var a in spell_attributes)
        {
            spell_names.Add(a.Key);
        }
        return spell_types;
    }

}

