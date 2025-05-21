using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

public class Relic
{
    public string name;
    public int sprite;
    public int triggerCounter;
    public RelicTriggerTimer triggerTimer;
    public RelicEffect effect;
    public string trigger_desc;
    public JObject attributes;
    public bool status;
    public SpellCaster caster;

    public Relic(JObject attributes)
    {
        this.attributes = attributes;
        this.name = attributes["name"].ToString();
        this.sprite = attributes["sprite"].ToObject<int>();
        this.triggerCounter = 0;
    }

    public virtual void Pickup()
    {
        this.caster = GameManager.Instance.player.GetComponent<PlayerController>().spellcaster;
        Debug.Log("spell caster is" + this.caster);
        SetEffect(attributes);
        SetTrigger(attributes);
    }

    public virtual void SetEffect(JObject attributes)
    {
        string effect_type = attributes["effect"]["type"].ToString();
        switch (effect_type)
        {
            case "gain-mana":
                this.effect = new GainManaEffect(attributes);
                break;
            case "gain-spellpower":
                this.effect = new GainSpellPowerEffect(attributes);
                break;
            case "gain-health":
                effect = new GainHealthEffect(attributes);
                break;
            case "take-damage":
                effect = new GlassCannon(attributes);
                break;
            default:
                this.effect = new RelicEffect(attributes);
                break;
        }
        this.effect.SetUntil();
        this.effect.SetCaster(this.caster);
    }

    public virtual void SetTrigger(JObject attributes)
    {
        string trigger_type = attributes["trigger"]["type"].ToString();
        string trigger_amount = attributes["trigger"]["amount"]?.ToString();
        switch (trigger_type)
        {
            case "take-damage":
                void OnDamageTrigger(Vector3 where, Damage dmg, Hittable target) {
                    if (!string.IsNullOrEmpty(trigger_amount))
                        CountTrigger(trigger_amount);
                    else
                        this.effect.ApplyEffect();
                }
                EventBus.Instance.OnDamage += OnDamageTrigger;
                break;
            case "stand-still":
                void OnMoveTrigger(Vector3 where, Hittable who) {
                    if (!string.IsNullOrEmpty(trigger_amount))
                        TimeTrigger(trigger_amount);
                    else
                        this.effect.ApplyEffect();
                }
                EventBus.Instance.OnMove += OnMoveTrigger;
                if (!string.IsNullOrEmpty(trigger_amount)){
                    Debug.Log("Time Trigger Amount: " + trigger_amount);
                    TimeTrigger(trigger_amount);
                }
                break;
            case "on-kill":
                void OnKillTrigger(Vector3 where, Hittable target) {
                    if (!string.IsNullOrEmpty(trigger_amount))
                        CountTrigger(trigger_amount);
                    else
                        this.effect.ApplyEffect();
                }
                EventBus.Instance.OnKill += OnKillTrigger;
                break;
            case "end-cast":
                void OnEndCastTrigger(SpellCaster caster, Spell spell) {
                    if (!string.IsNullOrEmpty(trigger_amount))
                        CountTrigger(trigger_amount);
                    else
                        this.effect.ApplyEffect();
                }
                EventBus.Instance.OnEndCast += OnEndCastTrigger;
                break;
            default:
                break;
        }
    }

    public virtual void CountTrigger (string amount) {
        triggerCounter++;
        if (triggerCounter >= GetRPNFloat(amount)) {
            triggerCounter = 0;
            this.effect.ApplyEffect();
        }
    }

    public virtual void TimeTrigger (string amount) {
        if (triggerTimer != null) {
            triggerTimer.OnTimerFinished -= this.effect.ApplyEffect;
            triggerTimer.Cancel();
            triggerTimer = null;
        }

        triggerTimer = new RelicTriggerTimer(GetRPNFloat(amount));
        Debug.Log("HIII!!");
        triggerTimer.OnTimerFinished += this.effect.ApplyEffect;
    }

    public string GetName()
    {
        return this.name;
    }
    public int GetSprite()
    {
        return this.sprite;
    }
    public bool IsActive()
    {
        return status;
    }

    public string GetDescription()
    {
        this.trigger_desc = "" + attributes["trigger"]["description"] + " " + attributes["effect"]["description"];
        return trigger_desc;
    }

    public float GetRPNFloat (string stat) {
        Debug.Log($"Caster's power: {caster.power}");
        return RPN.calculateRPNFloat(stat, new Dictionary<string, int> { { "wave", GameManager.Instance.currentWave }, { "power", caster.power } });
    }
    public int GetRPN (string stat) {
        Debug.Log($"Caster's power: {caster.power}");
        return RPN.calculateRPN(stat, new Dictionary<string, int> { { "wave", GameManager.Instance.currentWave }, { "power", caster.power } });
    }


}