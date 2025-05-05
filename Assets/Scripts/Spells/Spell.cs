using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;


public class Spell 
{
    public float last_cast;
    public SpellCaster owner;
    public Hittable.Team team;
    public string name;
    public string description;
    public int icon;
    public string mana_cost;
    public string damage;
    public Damage.Type damage_type;
    public string cooldown;
    public Projectile projectile;
    public bool isModifier = false;
    public ValueModifier mods;
    public Spell(SpellCaster owner)
    {
        this.owner = owner;
    }

    public string GetName()
    {
        return this.name;
        // return this.name;
    }

    public int GetManaCost()
    {
        return 1;
        //this.mana_cost;
        // return int.Parse(this.mana_cost);
    }

    public int GetDamage()
    {
        // need some calculateDamage()
        // return calculateDamage();
        return 100;
    }

    public float GetCooldown()
    {
        return 0.75f;
    }

    public virtual int GetIcon()
    {
        return this.icon;
    }
    public virtual bool IsModifierSpell() // able to be overrided by modifier spell
    {
       return false;
    }

    public bool IsReady()
    {
        return (last_cast + GetCooldown() < Time.time);
    }
    
    public virtual void SetAttributes(JObject attributes) {  
        this.name = attributes["name"].ToString();
        this.description = attributes["description"].ToString();
        this.icon = attributes["icon"].ToObject<int>();
        this.damage = attributes["damage"]["amount"].ToString();
        this.damage_type = Damage.TypeFromString(attributes["damage"]["type"].ToString());
        this.cooldown = attributes["cooldown"].ToString();
        this.projectile = attributes["projectile"].ToObject<Projectile>();
    }

    public virtual IEnumerator Cast (Vector3 where, Vector3 target, Hittable.Team team, ValueModifier mods) {
        ApplyMods(mods);
        CoroutineManager.Instance.Run((Cast(where, target, team)));
        yield return new WaitForEndOfFrame();
    }

    public virtual IEnumerator Cast (Vector3 where, Vector3 target, Hittable.Team team) {

        this.team = team;
        GameManager.Instance.projectileManager.CreateProjectile(0, "straight", where, target - where, 15f, OnHit);
        yield return new WaitForEndOfFrame();
    }

    void OnHit(Hittable other, Vector3 impact) {
        if (other.team != team) {
            other.Damage(new Damage(GetDamage(), Damage.Type.ARCANE));
        }

    }
    public void ApplyMods (ValueModifier mods) {
        
    }

    public void ApplyMod (string stat, List<string> stat_mods) {
        float value = RPN.calculateRPNFloat(stat, new Dictionary<string, float> {{"wave", GameManager.Instance.currentWave}});
        for (int i = 0; i < stat_mods.Count; i++) {

        }
    }

}
