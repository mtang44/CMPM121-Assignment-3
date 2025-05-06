using UnityEngine;
using System.Collections;
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
        return base.GetDamage(mods) * GetRPN(N);
    }

    public override IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team, ValueModifier mods) {
        this.team = team;
        Vector3 direction = (target - where);
        for (int i = 0; i < GetRPN(N); i++) {
            Vector3 randomDir = GetRandomDirection(direction, GetRPNFloat(spray));
             GameManager.Instance.projectileManager.CreateProjectile(sprite, GetTrajectory(mods), where, where + randomDir, GetSpeed(mods), MakeOnHit(mods), lifetime: GetRPNFloat(lifetime));
        }
        yield return new WaitForEndOfFrame();
    }

    public Vector3 GetRandomDirection(Vector3 centerDir, float coneWidth) {
        float angle = Mathf.Atan2(centerDir.y, centerDir.x) * Mathf.Rad2Deg;
        float halfWidth = coneWidth / 2f;
        float randomAngle = Random.Range(angle - halfWidth, angle + halfWidth);
        float rad = randomAngle * Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);

        return dir;
    }
}