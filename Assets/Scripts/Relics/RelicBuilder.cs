using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using Unity.VisualScripting;

public class RelicBuilder
{
    private JObject relic_attributes = new JObject();
    private List<string> relic_names = new List<string>();

    public RelicBuilder()
    {
        relic_attributes = ReadRelicsJson();
    }

    public Relic MakeRandomRelic(GameObject owner) {
        System.Random rnd = new System.Random();
        int index = rnd.Next(relic_names.Count);

        // create random spell
        Relic r = BuildRelic(relic_names[index]);
        // if spell is modifier then loop to create random spell again until we get a base spell
        Debug.Log("Our new relic's name: " + r.GetName());
        return r;
    }
    public Relic BuildRelic(string name)
    {
        Relic newRelic = MakeRelic(name);
        newRelic.SetAttributes((JObject)relic_attributes[name]);
        return newRelic;
    }
    private Relic MakeRelic(string name)
    {
        // need implementation of relic classes relic classes folder already created

        // if(name == "Green Gem")
        // {
        //       return new GreenGem();
        // }
        // if(name == "Jade Elephant")
        // {
        //     return new JadeElephant();
        // }
        // if(name == "Golden Mask")
        // {
        //     return new GoldenMask();
        // }
        // if(name == "Cursed Scroll")
        // {
        //     return new CursedScroll();
        // }
        return null;

    }

    public JObject ReadRelicsJson()
    {
            var relic_text = Resources.Load<TextAsset>("relics");
            JObject relic_types = JObject.Parse(relic_text.text);
            foreach(var a in relic_types)
            {
                relic_names.Add(a.Key);
            }
            return relic_types;
        }

        

}