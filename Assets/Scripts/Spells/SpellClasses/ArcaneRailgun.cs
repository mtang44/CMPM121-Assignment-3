using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

public class ArcaneRailgun : Spell {

    public ArcaneRailgun (SpellCaster owner) : base(owner) {

    }

    public override void SetAttributes(JObject attributes) {
        base.SetAttributes(attributes);
    }

    public override IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team) {
        yield return new WaitForEndOfFrame();
    }  
}