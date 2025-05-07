using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellCaster
{
    public int mana;
    public string max_mana_data;
    public int max_mana { get { return GetRPN(max_mana_data); } }
    public string mana_reg_data;
    public int mana_reg { get { return GetRPN(mana_reg_data); } }
    public string power_data;
    public int power { get { return GetRPN(power_data); } }
    public Hittable.Team team;
    public Spell spell;

    public IEnumerator ManaRegeneration()
    {
        while (true)
        {
            mana += mana_reg;
            mana = Mathf.Min(mana, max_mana);
            yield return new WaitForSeconds(1);
        }
    }

    public SpellCaster(string mana, string mana_reg, string power, Hittable.Team team)
    {
        this.max_mana_data = mana;
        this.mana = max_mana;
        this.mana_reg_data = mana_reg;
        this.power_data = power;
        this.team = team;
        spell = new SpellBuilder().MakeRandomSpell(this);
    }

    public IEnumerator Cast(Vector3 where, Vector3 target)
    {
        if (mana >= spell.GetManaCost() && spell.IsReady())
        {
            mana -= spell.GetManaCost();
            yield return spell.Cast(where, target, team);
        }
        yield break;
    }
    public float GetRPNFloat(string stat)
    {
        return RPN.calculateRPNFloat(stat, new Dictionary<string, float> { { "wave", GameManager.Instance.currentWave } });
    }
    public int GetRPN(string stat)
    {
        return RPN.calculateRPN(stat, new Dictionary<string, int> { { "wave", GameManager.Instance.currentWave } });
    }
}