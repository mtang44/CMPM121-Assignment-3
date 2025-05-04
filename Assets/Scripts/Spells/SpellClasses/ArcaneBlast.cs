using UnityEngine;
using System.Collections;
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
        secondary_projectile_lifetime = attributes["secondary_projectile"]["sprite"].ToString();
        projectile = attributes["projectile"].ToObject<Projectile>();
    }
    
    public override IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team) {
        yield return new WaitForEndOfFrame();
    }

}