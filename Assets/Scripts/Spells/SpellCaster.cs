using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellCaster 
{
    public int mana;
    public string max_mana_data;
    public int max_mana { get { return GetRPN(max_mana_data) + max_mana_mods;}}
    public int max_mana_mods = 0;
    public string mana_reg_data;
    public int mana_reg { get { return GetRPN(mana_reg_data) + mana_reg_mods;}}
    public int mana_reg_mods = 0;
    public string power_data;
    public int power { get { return GetRPN(power_data) + power_mods;}}
    public int power_mods = 0;
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
        this.spell = new SpellBuilder().MakeRandomSpell(this);

        GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.Add(spell);
        // SpellUI UIspell = new SpellUI();
        // UIspell.SetSpell(spell);
        // for(int i = 0; i < GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.Count;i++)
        // {
        //  GameManager.Instance.player.GetComponent<SpellUIContainer>().spellUIs[i] = UIspell;
        // }
        // GameManager.Instance.player.GetComponent<SpellUI>().SetSpell(spell);
       
    }
    public void SetSpell(Spell spell)
    {
        this.spell = spell;
    }
    public IEnumerator Cast(Vector3 where, Vector3 target)
    {        
        if (mana >= spell.GetManaCost() && spell.IsReady())
        {
            mana -= spell.GetManaCost();
            yield return spell.Cast(where, target, team);
        }
        if (this.team == Hittable.Team.PLAYER) {
            EventBus.Instance.DoEndCast(this, this.spell);
        }
        yield break;
    }

    public void GainMana(int amount)
    {
        mana = (mana + amount >= amount ? max_mana : mana + amount);
    }

    public void GainPower(int amount)
    {
        power_mods += amount;
    }
    


    public float GetRPNFloat(string stat)
    {
        return RPN.calculateRPNFloat(stat, new Dictionary<string, int> { { "wave", GameManager.Instance.currentWave } });
    }
    public int GetRPN (string stat) {
        return RPN.calculateRPN(stat, new Dictionary<string, int> {{"wave", GameManager.Instance.currentWave}});
    }
}
