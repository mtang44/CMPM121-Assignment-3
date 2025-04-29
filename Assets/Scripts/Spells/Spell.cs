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
    public Dictionary<string, string> projectile;

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
        return 1;
    }

    public bool IsReady()
    {
        return (last_cast + GetCooldown() < Time.time);
    }
     public virtual void SetAttributes(JObject attributes)
    {
       
        name = attributes["name"].ToString();
        description = attributes["description"].ToString();
        icon = attributes["icon"].ToObject<int>();
        damage = attributes["damage"]["amount"].ToString();
        damage_type = Damage.TypeFromString(attributes["damage"]["type"].ToString());
        cooldown = attributes["cooldown"].ToString();
        projectile = attributes["projectile"].ToObject<Projectile>();
      
      //...
    }
    public virtual IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team, ValueModifier? mods = null)
    {
        ValueModifier modifiers = mods ?? null;
        this.team = team;
        GameManager.Instance.projectileManager.CreateProjectile(0, "straight", where, target - where, 15f, OnHit);
        yield return new WaitForEndOfFrame();
    }

    void OnHit(Hittable other, Vector3 impact)
    {
        if (other.team != team)
        {
            other.Damage(new Damage(GetDamage(), Damage.Type.ARCANE));
        }

    }

}
