public class ArcaneBlast : Spell {

private string secondaryDamage;
private string N;
private string secondaryProjectile;
private string secondary_projectile_trajectory;
private string secondary_projectile_speed;
private string secondary_projectile_lifetime;
private int secondary_projectile_sprite;

    public ArcaneBlast () {
      
        
    }

    public void override SetAttributes(JObject attributes) {
        base.SetAttributes();
        N = attributes["N"].ToString();
        secondaryDamage = attributes["secondary_damage"].ToString();
        secondary_projectile_trajectory = attributes["secondary_projectile"]["trajectory"].ToString();
        secondary_projectile_speed = attributes["secondary_projectile"]["speed"].ToString();
        secondary_projectile_lifetime = attributes["secondary_projectile"]["lifetime"].ToString();
        secondary_projectile_lifetime = attributes["secondary_projectile"]["sprite"]ToObject<int>();
        projectile = attributes["projectile"].ToObject<Projectile>();
      
    }
    
}