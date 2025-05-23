using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System;

public class GainHealthEffect : RelicEffect
{

    Hittable hp;

    public GainHealthEffect(JObject attributes) : base(attributes)
    {
        this.hp = GameManager.Instance.player.GetComponent<PlayerController>().hp;
        GameManager.Instance.player.GetComponent<PlayerController>().EnableVampire();
    }

    public override void SetUntil()
    {
        base.SetUntil();
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        int amountToGain = GetRPN(amount);
        hp.Heal(amountToGain);
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }


}