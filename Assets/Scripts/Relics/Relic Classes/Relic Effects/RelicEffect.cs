using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using System;
public class RelicEffect
{

    public string description;
    public string type;
    public string amount;
    public string until;

    SpellCaster caster;

    public bool applied = false;

    public RelicEffect(JObject attributes)
    {
        this.description = attributes["effect"]["description"].ToString();
        this.type = attributes["effect"]["type"].ToString();
        this.amount = attributes["effect"]["amount"].ToString();
        this.until = attributes["effect"]["until"].ToString();
        this.caster = GameManager.Instance.player.GetComponent<PlayerController>().spellcaster;
    }

    public virtual void SetUntil()
    {
        if (string.IsNullOrEmpty(until))
        {
            return;
        }
        switch (until)
        {
            case "take-damage":
                void OnDamageUntil(Vector3 where, Damage dmg, Hittable target) => RemoveEffect();
                EventBus.Instance.OnDamage += OnDamageUntil;
                break;
            case "stand-still":
                void OnMoveUntil(Vector3 where, Hittable who) => RemoveEffect();
                EventBus.Instance.OnMove += OnMoveUntil;
                break;
            case "on-kill":
                void OnKillUntil(Vector3 where, Hittable target) => RemoveEffect();
                EventBus.Instance.OnKill += OnKillUntil;
                break;
            case "cast-spell":
                void OnCastUntil(SpellCaster caster, Spell spell) => RemoveEffect();
                EventBus.Instance.OnCast += OnCastUntil;
                break;
            default:
                break;
        }
    }

    public virtual void ApplyEffect()
    {
        applied = true;
    }

    public virtual void RemoveEffect()
    {
        applied = false;
    }  
    
    public virtual float GetRPNFloat (string stat) {
        return RPN.calculateRPNFloat(stat, new Dictionary<string, int> {{"wave", GameManager.Instance.currentWave}});
    }
    public virtual int GetRPN (string stat) {
        return RPN.calculateRPN(stat, new Dictionary<string, int> {{"wave", GameManager.Instance.currentWave}});
    }


}