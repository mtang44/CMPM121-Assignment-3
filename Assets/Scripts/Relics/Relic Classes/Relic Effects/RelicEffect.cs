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

    public SpellCaster caster;

    public bool applied = false;

    public RelicEffect(JObject attributes)
    {
        this.description = attributes["effect"]["description"].ToString();
        this.type = attributes["effect"]["type"].ToString();
        this.amount = attributes["effect"]["amount"].ToString();
        this.until = attributes["effect"]["until"]?.ToString();
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
            case "end-cast":
                void OnEndCastUntil(SpellCaster caster, Spell spell) => RemoveEffect();
                EventBus.Instance.OnEndCast += OnEndCastUntil;
                break;
            default:
                break;
        }
    }

    public void SetCaster(SpellCaster caster) {
        this.caster = caster;
    }

    public virtual void ApplyEffect()
    {
        applied = true;
        Debug.Log("Effect Applied!");
    }

    public virtual void RemoveEffect()
    {
        applied = false;
    }  
    
    public float GetRPNFloat (string stat) {
        return RPN.calculateRPNFloat(stat, new Dictionary<string, int> {{"wave", GameManager.Instance.currentWave}, {"power", caster.power}});
    }
    public int GetRPN (string stat) {
        return RPN.calculateRPN(stat, new Dictionary<string, int> {{"wave", GameManager.Instance.currentWave}, {"power", caster.power}});
    }

}