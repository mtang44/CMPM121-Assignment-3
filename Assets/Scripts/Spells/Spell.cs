using UnityEngine;
using System.Collections;
using System;
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

    public string trajectory;
    public string speed;
    public int sprite;
    public bool isModifier = false;
    public ValueModifier mods;
    public Spell(SpellCaster owner)
    {
        this.owner = owner;
    }

    public virtual string GetName()
    {
        return this.name;
        // return this.name;
    }



    public virtual int GetManaCost(ValueModifier mods) {
        Debug.Log("Final Mana Cost: " + (int)Math.Ceiling(ApplyStatMods(mods, this.mana_cost, "mana_cost")));
        return (int)Math.Ceiling(ApplyStatMods(mods, this.mana_cost, "mana_cost"));
    }

    public virtual int GetDamage(ValueModifier mods) {
        Debug.Log("Final Damage: " + (int)Math.Ceiling(ApplyStatMods(mods, this.damage, "damage")));
        return (int)Math.Ceiling(ApplyStatMods(mods, this.damage, "damage"));
    }

    public virtual float GetCooldown(ValueModifier mods) {
        Debug.Log("Final Cooldown: " + ApplyStatMods(mods, this.cooldown, "cooldown"));
        return ApplyStatMods(mods, this.cooldown, "cooldown");
    }

    public virtual float GetSpeed(ValueModifier mods) {
        Debug.Log("Final Speed: " + ApplyStatMods(mods, this.speed, "speed"));
        return ApplyStatMods(mods, this.speed, "speed");
    }

    public virtual string GetTrajectory(ValueModifier mods) {
        string currentTrajectory = this.trajectory;
        if (mods.modifiers.ContainsKey("trajectory")) {
            List<string> trajectoryMods = mods.modifiers["trajectory"];
            currentTrajectory = trajectoryMods[trajectoryMods.Count - 1];
        }
        return currentTrajectory;
    }

    public virtual int GetIcon() {
        return icon;
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
        this.mana_cost = attributes["mana_cost"].ToString();
        this.cooldown = attributes["cooldown"].ToString();
        this.trajectory = attributes["projectile"]["trajectory"].ToString();
        this.speed = attributes["projectile"]["speed"].ToString();
        this.sprite = attributes["projectile"]["sprite"].ToObject<int>();
    }

    public virtual IEnumerator Cast (Vector3 where, Vector3 target, Hittable.Team team, ValueModifier mods) {
        this.team = team;
        GameManager.Instance.projectileManager.CreateProjectile(sprite, GetTrajectory(mods), where, target - where, GetSpeed(mods), MakeOnHit(mods));
        yield return new WaitForEndOfFrame();
    }

    public virtual IEnumerator Cast (Vector3 where, Vector3 target, Hittable.Team team) {
        CoroutineManager.Instance.Run(Cast(where, target, team, new ValueModifier()));
        yield return new WaitForEndOfFrame();
    }

 
    public virtual Action<Hittable,Vector3> MakeOnHit(ValueModifier mods)
    {
        void OnHit(Hittable other, Vector3 impact) {
            if (other.team != team) {
                other.Damage(new Damage(GetDamage(mods), damage_type));
            }
        }
        return OnHit;
    }
    
    void OnHit(Hittable other, Vector3 impact) {
        if (other.team != team) {
            other.Damage(new Damage(GetDamage(), damage_type));
        }
    }

    public float ApplyStatMods (ValueModifier mods, string stat, string stat_name) {
        float value = GetRPNFloat(stat);
        value = ApplyAdd(mods, value, stat_name + "_add");
        value = ApplyMult(mods, value, stat_name + "_mult");
        return value;
    }

    public float ApplyAdd (ValueModifier mods, float val, string mod_name) {
        float value = val;
        if (mods.modifiers.ContainsKey(mod_name)) {
            for (int i = 0; i < mods.modifiers[mod_name].Count; i++) {
                Debug.Log("ApplyAdd Current Mod to Add: " + mods.modifiers[mod_name][i]);
                value += GetRPNFloat(mods.modifiers[mod_name][i]);
            }
        }
        return value;
    }

    public float ApplyMult (ValueModifier mods, float val, string mod_name) {
        float value = val;
         if (mods.modifiers.ContainsKey(mod_name)) {
            for (int i = 0; i < mods.modifiers[mod_name].Count; i++) {
                Debug.Log("ApplyMult Current Mod to Mult: " + mods.modifiers[mod_name][i]);
                value *= GetRPNFloat(mods.modifiers[mod_name][i]);
            }
        }
        return value;
    }

    public float GetRPNFloat (string stat) {
        return RPN.calculateRPNFloat(stat, new Dictionary<string, int> {{"wave", GameManager.Instance.currentWave}, {"power", owner.power}});
    }
    public int GetRPN (string stat) {
        return RPN.calculateRPN(stat, new Dictionary<string, int> {{"wave", GameManager.Instance.currentWave}, {"power", owner.power}});
    }

    
    public int GetManaCost() { return GetManaCost(new ValueModifier()); }
    public int GetDamage() { return GetDamage(new ValueModifier()); }
    public float GetCooldown() { return GetCooldown(new ValueModifier()); }
    public float GetSpeed() { return GetSpeed(new ValueModifier()); }
    public string GetTrajectory() { return GetTrajectory(new ValueModifier()); }

}
