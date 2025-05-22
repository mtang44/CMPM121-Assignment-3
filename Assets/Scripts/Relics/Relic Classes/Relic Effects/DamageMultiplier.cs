using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DamageMultiplier : RelicEffect
{

    Hittable hp;
    public DamageMultiplier(JObject attributes) : base(attributes)
    {
        hp = GameManager.Instance.player.GetComponent<PlayerController>().hp;
        GameManager.Instance.player.GetComponent<PlayerController>().speedMult *= 1.5f;
    }
    public override void SetUntil()
    {
        base.SetUntil();
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        int calcDamage = RPN.calculateRPN(amount, new Dictionary<string, int> { ["baseDamage"] = hp.d_taken });
        Debug.Log("Additional damage: " + calcDamage);
        calcDamage *= -1;
        hp.Heal(calcDamage);
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }


}