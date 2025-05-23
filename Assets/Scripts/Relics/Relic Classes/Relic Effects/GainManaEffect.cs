using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System;

public class GainManaEffect : RelicEffect
{

    public GainManaEffect(JObject attributes) : base(attributes)
    {

    }
    
    public override void SetUntil()
    {
        base.SetUntil();
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        caster.GainMana(GetRPN(amount));
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }  


}