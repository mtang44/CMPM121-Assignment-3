using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System;

public class GainSpellPowerEffect : RelicEffect
{

    public GainSpellPowerEffect(JObject attributes) : base(attributes)
    {
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        caster.GainPower(GetRPN(amount));
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
        caster.GainPower(-GetRPN(amount));
    }  


}