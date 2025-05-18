using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System;

public class GainSpellPowerEffect : RelicEffect
{

    SpellCaster caster;

    public GainSpellPowerEffect(JObject attributes) : base(attributes)
    {
        this.caster = GameManager.Instance.player.GetComponent<PlayerController>().spellcaster;
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        this.caster.GainPower(GetRPN(amount));
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
        this.caster.GainPower(-GetRPN(amount));
    }  


}