using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;
using System.Random;


public class SpellBuilder 
{
    private Dictionary <string, JObject> spell_attributes;
    private List <string> spell_names;


    public Spell BuildSpell(string name, SpellCaster owner)
    {
        Spell spell = MakeSpell(name);
        spell.SetAttributes(spell_attributes[name]);
        return spell;
    }

   
    public SpellBuilder()
    {        
        spell_attributes = readSpellsJson();
    }

    private Spell MakeSpell(string name) {
        if (name == "magic_missile") {
            return new MagicMissile();
        }
        if(name == "arcane_blast"){
            return new ArcaneBlast();
        }
        if(name == "arcane_spray"){
            return new ArcaneSpray();
        }
        if(name == "arcane_bolt"){
            return new ArcaneBolt();
        }
        if(name == "damage-amplified"){ 
            return new DamageAmp();
        }
        if(name == "speed-amplified"){
            return new SpeedAmp();
        }
        if(name == "doubled"){// not made
            return new Doubler();
        }
        if(name == "split"){// not made
            return new Splitter();
        }
        if(name == "chaotic"){// not made
            return new chaotic();
        }
        if(name == "homing"){ // not made
            return new homing();
        }

    }
    public Spell makeRandomSpell()
    {
        Random rnd = new Random();
        int index = rnd.Next(spell_names.Count);

        // create random spell
        Spell s = buildSpell(spell_names[index]);
        // if spell is modifier then loop to create random spell again until we get a base spell
        if (s.IsModifierSpell())
        {
            s.AddChild(makeRandomSpell(spell_names))
        }
        return s;
    }

    public Dictionary<string, JObject> readSpellsJson()
     {
        Dictionary<string, JObject> spell_types = new Dictionary<string, JObject>();
        var spelltext = Resources.Load<TextAsset>("spells");
        JObject spell_types = JObject.Parse(spelltext.text);
        foreach(var a in spell_types)
        {
            spell_names.Add(a.Key);
        }
        return spell_types;
    }
}

