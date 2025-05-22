using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GlassCannon : RelicEffect
{

    Hittable hp;
    public GlassCannon(JObject attributes) : base(attributes)
    {
        this.hp = GameManager.Instance.player.GetComponent<PlayerController>().hp;
        GameManager.Instance.player.GetComponent<PlayerController>().speedMult *= 1.5f;
    }
    public override void SetUntil()
    {
        base.SetUntil();
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        //int calcDamage = RPN.calculateRPN(amount, new Dictionary<string, int> { ["wave"] = GameManager.Instance.currentWave });
        Damage damage = new Damage(2, Damage.Type.DARK);
        hp.Damage(damage);
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }


}