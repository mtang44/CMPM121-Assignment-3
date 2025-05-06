using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

public class ArcaneRailgun : Spell {

    public string pierce;

    public ArcaneRailgun (SpellCaster owner) : base(owner) {

    }

    public override void SetAttributes(JObject attributes) {
        base.SetAttributes(attributes);
        pierce = attributes["pierce"].ToString();
    }

    public override IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team, ValueModifier mods) {
        this.team = team;
        GameManager.Instance.projectileManager.CreateProjectile(sprite, GetTrajectory(mods), where, target - where, GetSpeed(mods), MakeOnHit(mods), pierce: GetRPN(pierce));
        yield return new WaitForEndOfFrame();
    }  
}