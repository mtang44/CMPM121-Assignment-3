using UnityEngine;
using System;
using Newtonsoft.Json.Linq;

public class ChainLightning : Spell {
    private string recast_offset_max;

    public ChainLightning(SpellCaster owner) : base(owner) {
      
        
    }

    public override void SetAttributes(JObject attributes) {
        base.SetAttributes(attributes);
        recast_offset_max = attributes["recast_offset_max"].ToString();
    }
    
    public override Action<Hittable,Vector3> MakeOnHit(ValueModifier mods)
    {
        void OnHit(Hittable other, Vector3 impact) {
            if (other.team != team) {
                // Debug.Log("Recasting!");
                float recastOffsetMax = GetRPNFloat(recast_offset_max);

                // Launch other projectile
                Vector3 hitPosition = other.owner.transform.position;
                Vector3 newPosition = hitPosition + new Vector3(UnityEngine.Random.Range(-recastOffsetMax, recastOffsetMax), UnityEngine.Random.Range(-recastOffsetMax, recastOffsetMax), 0);
                Vector3 closest = GameManager.Instance.GetClosestEnemy(newPosition).transform.position;
                GameManager.Instance.projectileManager.CreateProjectile(sprite, trajectory, newPosition, closest - newPosition, GetRPNFloat(speed), SecondaryOnHit(mods));

                other.Damage(new Damage(GetDamage(mods), damage_type));
            }
        }
        return OnHit;
    }

    public Action<Hittable,Vector3> SecondaryOnHit(ValueModifier mods)
    {
        void OnHit(Hittable other, Vector3 impact) {
            // Debug.Log("Recasting!");
            float recastOffsetMax = GetRPNFloat(recast_offset_max);

            // Launch other projectile
            Vector3 hitPosition = other.owner.transform.position;
            Vector3 newPosition = hitPosition + new Vector3(UnityEngine.Random.Range(-recastOffsetMax, recastOffsetMax), UnityEngine.Random.Range(-recastOffsetMax, recastOffsetMax), 0);
            Vector3 closest = GameManager.Instance.GetClosestEnemy(newPosition).transform.position;
            GameManager.Instance.projectileManager.CreateProjectile(sprite, trajectory, newPosition, closest - newPosition, GetRPNFloat(speed), SecondaryOnHit(mods));

            other.Damage(new Damage(GetDamage(mods), damage_type));
        }
        return OnHit;
    }

}