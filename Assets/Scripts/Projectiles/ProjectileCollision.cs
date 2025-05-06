using UnityEngine;
using System.Collections.Generic;
public class ProjectileCollision
{
    public int max_collisions;
    public int collision_count;

    public List<GameObject> collided = new List<GameObject>();

    public ProjectileCollision()
    {
        this.collision_count = 0;
        this.max_collisions = 0;
    }

    public void AddPierce (int pierce) {
        max_collisions = pierce;
    }
    
    public bool LogCollision (GameObject hit) {
        if (collided.Contains(hit)) {
            return true;
        }
        collided.Add(hit);
        collision_count++;
        return false;
    }

    public bool HitLimit () {
        return collision_count >= max_collisions;
    }



}
