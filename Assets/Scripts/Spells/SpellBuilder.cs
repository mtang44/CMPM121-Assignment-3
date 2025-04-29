using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;


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
        readSpellsJson();
    }

    private Spell MakeSpell(string name) {
        if (name == "magic_missile") {
            return new MagicMissile();
        }
        if(name == "arcane_blast"){
            return new arcaneBlact();
        }
        if(name == "arcane_spray"){
            return new ArcaneSpray();
        }
        if(name == "arcane_bolt"){
            return new arcaneBolt();
        }
        if(name == "damage-amplified"){
            return new damageAmplifier();
        }
        if(name == "speed-amplified"){
            return new speedAmplified();
        }
        if(name == "doubled"){
            return new doubled();
        }
        if(name == "split"){
            return new split();
        }
        if(name == "chaotic"){
            return new chaotic();
        }
        if(name == "homing"){
            return new homing();
        }
    }

    public void readSpellsJson()
     {
        var spelltext = Resources.Load<TextAsset>("spells");
        
        spell_attributes = JObject.Parse(spelltext.text);
        foreach(var a in spell_attributes)
        {
            spell_names.Add(a.Key);
        }
    }
}

