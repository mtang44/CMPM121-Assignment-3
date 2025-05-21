using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System;

public class GlassCannonBuff : RelicEffect
{

    Hittable hp;
    HealthBar healthui;

    public GlassCannonBuff(JObject attributes) : base(attributes)
    {
        this.hp = GameManager.Instance.player.GetComponent<PlayerController>().hp;
        this.healthui = GameManager.Instance.player.GetComponent<PlayerController>().healthui;
        GameManager.Instance.player.GetComponent<PlayerController>().speedMult *= 1.5f;
    }


    public override void SetUntil() // This is a permanent buff! There is no escape :)
    {
        base.SetUntil();
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        float damageNum = GetRPNFloat(amount);
        Damage additionalDamage = new Damage((int)damageNum, Damage.Type.DARK);
        hp.Damage(additionalDamage);
        healthui.SetHealth(hp);
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }


}