using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System;

public class GlassCannonBuff : RelicEffect
{

    Hittable hp;
    HealthBar healthui;
    int baseDamage;

    public GlassCannonBuff(JObject attributes) : base(attributes)
    {
        this.hp = GameManager.Instance.player.GetComponent<PlayerController>().hp;
        this.healthui = GameManager.Instance.player.GetComponent<PlayerController>().healthui;
        GameManager.Instance.player.GetComponent<PlayerController>().speedMult *= 1.5f;
        this.baseDamage = GameManager.Instance.player.GetComponent<PlayerController>().baseDamage;
    }


    public override void SetUntil() // This is a permanent buff! There is no escape :)
    {
        base.SetUntil();
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        int damageNum = RPN.calculateRPN(amount, new Dictionary<string, int> { ["baseDamage"] = this.baseDamage});
        Damage additionalDamage = new Damage(damageNum, Damage.Type.DARK);
        hp.Damage(additionalDamage);
        healthui.SetHealth(hp);
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }


}