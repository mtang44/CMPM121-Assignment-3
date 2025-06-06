using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

public class ArcaneBlast : Spell {

    private string N;

    private string secondary_damage;
    private string secondary_projectile_trajectory;
    private string secondary_projectile_speed;
    private string secondary_projectile_lifetime;
    private int secondary_projectile_sprite;

    public ArcaneBlast (SpellCaster owner) : base(owner) {
      
        
    }

    public override void SetAttributes(JObject attributes) {
        base.SetAttributes(attributes);
        N = attributes["N"].ToString();
        secondary_damage = attributes["secondary_damage"].ToString();
        secondary_projectile_trajectory = attributes["secondary_projectile"]["trajectory"].ToString();
        secondary_projectile_speed = attributes["secondary_projectile"]["speed"].ToString();
        secondary_projectile_lifetime = attributes["secondary_projectile"]["lifetime"].ToString();
        secondary_projectile_sprite = attributes["secondary_projectile"]["sprite"].ToObject<int>();
    }
    
    public override Action<Hittable,Vector3> MakeOnHit(ValueModifier mods)
    {
        void OnHit(Hittable other, Vector3 impact) {
            if (other.team != team) {
                other.Damage(new Damage(GetDamage(mods), damage_type));
                int n = GetRPN(N);
                for (int i = 0; i < n; i++) {
                    float angle = (360f / n) * i;
                    GameManager.Instance.projectileManager.CreateProjectile(secondary_projectile_sprite, secondary_projectile_trajectory, impact, new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0), GetRPNFloat(secondary_projectile_speed), SecondaryOnHit(mods), lifetime: GetRPNFloat(secondary_projectile_lifetime));
                }  
            }
        }
        return OnHit;
    }

    public Action<Hittable,Vector3> SecondaryOnHit(ValueModifier mods)
    {
        void OnHit(Hittable other, Vector3 impact) {
            if (other.team != team) {
                other.Damage(new Damage((int)Math.Ceiling(ApplyStatMods(mods, this.secondary_damage, "damage")), damage_type));
            }
        }
        return OnHit;
    }

}