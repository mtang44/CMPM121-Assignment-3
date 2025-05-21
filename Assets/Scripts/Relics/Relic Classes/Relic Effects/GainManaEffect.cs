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
        Debug.Log("Mana before: " + caster.mana);
        this.caster.GainMana(GetRPN(amount));
        Debug.Log("Gained mana: " + GetRPN(amount));
        Debug.Log("Mana after: " + caster.mana);
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }  


}