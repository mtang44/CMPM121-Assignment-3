using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;

public class ModifierSpell : Spell {
    public Spell child;
    public int child_count = 0;

    public ModifierSpell (SpellCaster owner) : base(owner) {

    }

    public override bool IsModifierSpell()
    {
        return true;
    }

    public override int GetManaCost(ValueModifier mods) {
        return child.GetManaCost(AddMods(mods));
    }

    public override int GetDamage(ValueModifier mods) {
        return child.GetDamage(AddMods(mods));
    } 

    public override float GetCooldown(ValueModifier mods) {
        return child.GetCooldown(AddMods(mods));
    }
    
    public override float GetSpeed(ValueModifier mods) {
        return child.GetSpeed(AddMods(mods));
    }

    public override string GetTrajectory(ValueModifier mods) {
        return child.GetTrajectory(AddMods(mods));
    }

    public override int GetIcon() {
        return child.GetIcon();
    }

    public override string GetName() {
        return this.name + " " + child.GetName();
    }
    

    public virtual ValueModifier AddMods() {
        return AddMods(new ValueModifier());
    }

    public virtual ValueModifier AddMods(ValueModifier mods) {
        return mods;
    }

    public int GetChildCount () {
        int count = child_count;
        if (child != null && child is ModifierSpell modifierSpell) {
            count += modifierSpell.GetChildCount();
        }
        return count;
    }

    public override IEnumerator Cast (Vector3 where, Vector3 target, Hittable.Team team, ValueModifier current_mods) {
        Debug.Log(GetName());
        CoroutineManager.Instance.Run(this.child.Cast(where, target, team, AddMods(current_mods)));
        yield return new WaitForEndOfFrame();
        // this.team = team;
        // GameManager.Instance.projectileManager.CreateProjectile(0, "straight", where, target - where, 15f, OnHit);
        // yield return new WaitForEndOfFrame();
    }

    public override IEnumerator Cast (Vector3 where, Vector3 target, Hittable.Team team) {
        CoroutineManager.Instance.Run(Cast(where, target, team, new ValueModifier()));
        yield return new WaitForEndOfFrame();
    }

    public void AddChild(Spell childSpell)
    {
        this.child = childSpell;
        this.child_count++;
    }

    public override void SetAttributes(JObject attributes)
    {
        name = attributes["name"].ToString();
        description = attributes["description"].ToString();
        
    }
}