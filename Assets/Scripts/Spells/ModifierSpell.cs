using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public classs ModifierSpell : Spell {
    public Spell child;
    public int child_count;
    public ValueModifier total_mods;
    private string name;
    private string description;

    public ModifierSpell (SpellCaster owner, ValueModifier? current_mods = null) : base(owner) {
        if (current_mods != null) {
            total_mods = current_mods;
        } else {
            total_mods = new ValueModifier;
        }
    }

    public virtual void AddMods () {
        // add specific multipliers to total mods.
    }

    public int GetChildCount () {
        if (child_count > 1) {
            return child_count + child.GetChildCount();
        } else {
            return child_count;
        }
    }
    IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team)
    {
        
        this.team = team;
        GameManager.Instance.projectileManager.CreateProjectile(0, "straight", where, target - where, 15f, OnHit);
        yield return new WaitForEndOfFrame();
    }
    public void AddChild(Spell childSpell)
    {
        this.child = childSpell;
    }
    public virtual void SetAttributes(JObject attributes)
    {
        name = attributes["name"].ToString();
        description = attributes["description"].ToString();
        
    }
}