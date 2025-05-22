using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class IgnoreDamageEffect : RelicEffect
{

    Hittable hp;

    public IgnoreDamageEffect(JObject attributes) : base(attributes)
    {
        this.hp = GameManager.Instance.player.GetComponent<PlayerController>().hp;
    }

    public override void SetUntil()
    {
        base.SetUntil();
    }

    public override void ApplyEffect()
    {
        if (UnityEngine.Random.value < 0.5f)
        {
            base.ApplyEffect();
            int amountToGain = RPN.calculateRPN(amount, new Dictionary<string, int> { ["base"] = hp.d_taken });
            hp.Heal(amountToGain);
        }
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }


}