using System.Collections.Generic;
using UnityEngine;

public class PlayerClass
{
    private string name;
    private int sprite;
    private string health;
    private string mana_regeneration;
    private string mana;
    private string spellpower;
    private string speed;

    public PlayerClass(string name, int sprite, string health, string mana_regeneration, string mana, string spellpower, string speed)
    {
        this.name = name;
        this.sprite = sprite;
        this.health = health;
        this.mana_regeneration = mana_regeneration;
        this.mana = mana;
        this.spellpower = spellpower;
        this.speed = speed;
    }

    public string getName()
    {
        return name;
    }
    public int getSprite()
    {
        return sprite;
    }
    public string getHealth()
    {
        return health;
    }
    public string getManaRegeneration()
    {
        return mana_regeneration;
    }
    public string getMana()
    {
        return mana;
    }
    public string getSpellpower()
    {
        return spellpower;
    }
    public string getSpeed()
    {
        return speed;
    }
}
