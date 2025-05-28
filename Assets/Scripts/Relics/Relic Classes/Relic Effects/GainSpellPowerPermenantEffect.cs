using Newtonsoft.Json.Linq;
using UnityEngine;

public class GainSpellPowerPermenantEffect : RelicEffect
{
    public GainSpellPowerPermenantEffect(JObject attributes) : base(attributes)
    {
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        caster.GainPower(GetRPN(amount));

    }

    public override void RemoveEffect()
    {
    }


}
