using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using Unity.VisualScripting;

public class Relic
{
    public string name;
    public int sprite;
    
    public Trigger trigger;
    public Effect effect; 



    public  Relic()
    {

    }
    public void pickup()
    {

    }
    public virtual void SetAttributes(JObject attributes) { 
        this.name = attributes["name"].ToString();
        this.sprite = attributes["icon"].ToObject<int>();
        this.trigger = trigger.setAttributes(attributes);
        this.effect = effect.setAttributes(attributes);

    }
    public string GetName()
    {
        return this.name;
    }
    public int GetSprite()
    {
        return this.sprite;
    } 
    public string GetDescription()
    {
        return "" +this.trigger.description +" " + this.effect.description;
    }




}