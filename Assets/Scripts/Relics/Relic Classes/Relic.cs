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
    public RelicEffect effect;
    public string trigger_desc;
    public JObject attributes;

    public Relic(JObject attributes)
    {
        this.attributes = attributes;
        this.name = attributes["name"].ToString();
        this.sprite = attributes["sprite"].ToObject<int>();
    }

    public virtual void Pickup()
    {
        SetEffect(attributes);
        SetTrigger(attributes);
    }

    public virtual void SetEffect(JObject attributes)
    {
        string effect_type = attributes["effect"]["type"].ToString();
        switch (effect_type)
        {
            case "gain-mana":
                effect = new GainManaEffect(attributes);
                break;
            case "gain-spellpower":
                effect = new GainSpellPowerEffect(attributes);
                break;
            case "gain-health":
                effect = new GainHealthEffect(attributes);
                break;
            default:
                effect = new RelicEffect(attributes);
                break;
        }
        effect.SetUntil();
    }

    public virtual void SetTrigger(JObject attributes)
    {
        string trigger_type = attributes["trigger"]["type"].ToString();
        switch (trigger_type)
        {
            case "take-damage":
                void OnDamageTrigger(Vector3 where, Damage dmg, Hittable target) => effect.ApplyEffect();
                EventBus.Instance.OnDamage += OnDamageTrigger;
                break;
            case "stand-still":
                void OnMoveTrigger(Vector3 where, Hittable who) => effect.ApplyEffect();
                EventBus.Instance.OnMove += OnMoveTrigger;
                break;
            case "on-kill":
                void OnKillTrigger(Vector3 where, Hittable target) => effect.ApplyEffect();
                EventBus.Instance.OnKill += OnKillTrigger;
                break;
            case "cast-spell":
                void OnCastTrigger(SpellCaster caster, Spell spell) => effect.ApplyEffect();
                EventBus.Instance.OnCast += OnCastTrigger;
                break;
            default:
                break;
        }
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
        this.trigger_desc = "" + attributes["trigger"]["description"] +" " + attributes["effect"]["description"];
        return trigger_desc;
    }


}