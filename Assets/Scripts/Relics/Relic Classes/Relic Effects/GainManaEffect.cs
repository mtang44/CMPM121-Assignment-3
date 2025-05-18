using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System;

public class GainManaEffect : RelicEffect
{

    SpellCaster caster;

    public GainManaEffect(JObject attributes) : base(attributes)
    {
        this.caster = GameManager.Instance.player.GetComponent<PlayerController>().spellcaster;
    }
    
    public override void SetUntil()
    {
        base.SetUntil();
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        this.caster.GainMana(GetRPN(amount));
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }  


}