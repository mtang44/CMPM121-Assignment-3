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

    public override int GetManaCost() {
        ValueModifier total_mods = AddMods();
        return child.GetManaCost(total_mods);
    }
    public override int GetManaCost(ValueModifier current_mods) {
        ValueModifier total_mods = AddMods(current_mods);
        return child.GetManaCost(total_mods);
    }

    public override int GetDamage() {
        ValueModifier total_mods = AddMods();
        return child.GetDamage(total_mods);
    }
    public override int GetDamage(ValueModifier current_mods) {
        ValueModifier total_mods = AddMods(current_mods);
        return child.GetDamage(total_mods);
    }

    public override int GetIcon() {
        return child.GetIcon();
    }

    public override string GetName() {
        return this.name + " " + child.GetName();
    }
    
    public virtual ValueModifier AddMods() {
        ValueModifier total_mods = new ValueModifier();
        total_mods = AddMods(total_mods);
        return total_mods;
    }    

    public virtual ValueModifier AddMods (ValueModifier mods) {
        ValueModifier total_mods = mods;
        total_mods = AddMods(total_mods);
        return total_mods;
    }

    public int GetChildCount () {
        int count = child_count;
        if (child != null && child is ModifierSpell modifierSpell) {
            count += modifierSpell.GetChildCount();
        }
        return count;
    }

    public override IEnumerator Cast (Vector3 where, Vector3 target, Hittable.Team team, ValueModifier current_mods) {
        ValueModifier total_mods = current_mods;
        total_mods = AddMods(total_mods);
        CoroutineManager.Instance.Run(this.child.Cast(where, target, team, total_mods));
        yield return new WaitForEndOfFrame();
        // this.team = team;
        // GameManager.Instance.projectileManager.CreateProjectile(0, "straight", where, target - where, 15f, OnHit);
        // yield return new WaitForEndOfFrame();
    }

    public override IEnumerator Cast (Vector3 where, Vector3 target, Hittable.Team team) {
        ValueModifier total_mods = new ValueModifier();
        total_mods = AddMods(total_mods);
        CoroutineManager.Instance.Run(this.child.Cast(where, target, team, total_mods));
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