using Newtonsoft.Json.Linq;
using UnityEngine;

public class GainHealthUnfuckedEffect : RelicEffect
{

    Hittable hp;

    public GainHealthUnfuckedEffect(JObject attributes) : base(attributes)
    {
        this.hp = GameManager.Instance.player.GetComponent<PlayerController>().hp;
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
