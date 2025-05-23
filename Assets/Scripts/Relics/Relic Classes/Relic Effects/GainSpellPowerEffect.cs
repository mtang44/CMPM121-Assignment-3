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
        if (base.applied == true)
        {
            caster.GainPower(-GetRPN(amount)); // was - GetRPN
        }
         base.RemoveEffect();
    }  


}