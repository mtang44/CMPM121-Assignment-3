using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System;

public class GainHealthEffect : RelicEffect
{

    Hittable hp;
    HealthBar healthui;

    public GainHealthEffect(JObject attributes) : base(attributes)
    {
        this.hp = GameManager.Instance.player.GetComponent<PlayerController>().hp;
        this.healthui = GameManager.Instance.player.GetComponent<PlayerController>().healthui;
        GameManager.Instance.player.GetComponent<PlayerController>().vampire = true;
    }

    public override void SetUntil()
    {
        base.SetUntil();
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        int amountToGain = GetRPN(amount);
        hp.hp += amountToGain;
        healthui.SetHealth(hp);
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }


}