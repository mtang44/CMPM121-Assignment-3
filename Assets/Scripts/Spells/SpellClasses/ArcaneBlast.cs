using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

public class ArcaneBlast : Spell {

private string secondaryDamage;
private string N;
private string secondaryProjectile;
private string secondary_projectile_trajectory;
private string secondary_projectile_speed;
private string secondary_projectile_lifetime;
private int secondary_projectile_sprite;

    public ArcaneBlast (SpellCaster owner) : base(owner) {
      
        
    }

    public override void SetAttributes(JObject attributes) {
        base.SetAttributes(attributes);
        N = attributes["N"].ToString();
        secondaryDamage = attributes["secondary_damage"].ToString();
        secondary_projectile_trajectory = attributes["secondary_projectile"]["trajectory"].ToString();
        secondary_projectile_speed = attributes["secondary_projectile"]["speed"].ToString();
        secondary_projectile_lifetime = attributes["secondary_projectile"]["lifetime"].ToString();
        secondary_projectile_sprite = attributes["secondary_projectile"]["sprite"].ToObject<int>();
    }

     public override int GetDamage(ValueModifier mods) {
        return base.GetDamage(mods) + ((int)Math.Ceiling(GetRPNFloat(secondaryDamage)) * GetRPN(N));
    }
    
    public override IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team, ValueModifier mods) {
        GameManager.Instance.projectileManager.CreateProjectile(sprite, GetTrajectory(mods), where, target - where, GetSpeed(mods), MakeOnHit(mods));
        yield return new WaitForEndOfFrame();
    }

    public override Action<Hittable,Vector3> MakeOnHit(ValueModifier mods)
    {
        void OnHit(Hittable other, Vector3 impact) {
            if (other.team != team) {
                other.Damage(new Damage(GetDamage(mods), Damage.Type.ARCANE));
                int n = GetRPN(N);
                for (int i = 0; i < n; i++) {
                    float angle = (360f / n) * i;
                    CoroutineManager.Instance.Run(Cast(impact, impact + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0), team, mods));
                }  
            }
        }
        return OnHit;
    }

}