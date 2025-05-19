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
        return new Relic((JObject)relic_attributes[name]);
    }

    public JObject ReadRelicsJson()
    {
        var relic_text = Resources.Load<TextAsset>("relics");
        JArray relic_types = JArray.Parse(relic_text.text);
        JObject relicObject = new JObject();
        foreach (var r in relic_types)
        {

            relic_names.Add(r["name"].ToString());
            string name = r["name"].ToString();
            relicObject[name] = r;

        }
        return relicObject;
    }

        

}