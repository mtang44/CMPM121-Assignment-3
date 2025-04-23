using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;


public class SpellBuilder 
{
    private Dictionary <string, Spell> spell_list;
    public Spell Build(SpellCaster owner)
    {
        return new Spell(owner);
    }

   
    public SpellBuilder()
    {        
    }
    public Dictionary<string, Spell> readSpellsJson()
     {
        Dictionary<string, Spell> spell_types = new Dictionary<string, Spell>();
        var spelltext = Resources.Load<TextAsset>("spells");
        
        JToken jo = JToken.Parse(spelltext.text);
        foreach (var spell in jo)
        {
           
            Spell s = spell.ToObject<Spell>();// request construction of object NEED Enemy class first
            spell_types[s.GetName()] = s;
        }
        return spell_types;
     }

}
