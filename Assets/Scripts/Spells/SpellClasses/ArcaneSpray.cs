using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

public class ArcaneSpray : Spell {

    private string N;
    private string spray;
    private string lifetime;
    public ArcaneSpray (SpellCaster owner) : base(owner) {

    }

    public override void SetAttributes(JObject attributes) {
        base.SetAttributes(attributes);
        N = attributes["N"].ToString();
        spray = attributes["spray"].ToString();
        lifetime = attributes["projectile"]["lifetime"].ToString();
    }

    public override int GetDamage(ValueModifier mods) {
        //Debug.Log("Final Damage: " + (int)Math.Ceiling(ApplyStatMods(mods, this.damage, "damage")) * GetRPN(N));
        return (int)Math.Ceiling(ApplyStatMods(mods, this.damage, "damage") * GetRPN(N));
    }

    public override IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team, ValueModifier mods) {
        this.team = team;
        last_cast = Time.time;
        Vector3 direction = target - where;
        float centerAngle = Mathf.Atan2(direction.y, direction.x);
        float halfCone = (GetRPNFloat(spray)) * 360f * Mathf.Deg2Rad / 2f;

        for (int i = 0; i < GetRPN(N); i++) {
            float randomOffset = UnityEngine.Random.Range(-halfCone, halfCone);
            float finalAngle = centerAngle + randomOffset;
            Vector3 dir = new Vector3(Mathf.Cos(finalAngle), Mathf.Sin(finalAngle), 0);
            GameManager.Instance.projectileManager.CreateProjectile(sprite, GetTrajectory(mods), where, dir, GetSpeed(mods), MakeOnHit(mods), lifetime: GetRPNFloat(lifetime));
        }

        yield return new WaitForEndOfFrame();
    }

    public override Action<Hittable,Vector3> MakeOnHit(ValueModifier mods)
    {
        void OnHit(Hittable other, Vector3 impact) {
            if (other.team != team) {
                other.Damage(new Damage((int)(GetDamage(mods) / GetRPN(N)), damage_type));
            }
        }
        return OnHit;
    }

}