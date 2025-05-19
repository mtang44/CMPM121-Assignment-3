using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Hittable hp;
    public HealthBar healthui;
    public ManaBar manaui;

    public PlayerClass player_class;
    public Dictionary<string, PlayerClass> class_stats = new Dictionary<string, PlayerClass>();

    public SpellCaster spellcaster;
    public SpellUI spellui; // need a list of spellUI
    public SpellUIContainer spellContainer;
    public GameObject activeSpell;
    public RewardScreenManager rewardscreen;

    public int speed;

    public Unit unit;
    

    // public List<SpellUI> activeSpellsUI = new List<SpellUI>();// stores spell for UI display while playing

    public List<Spell> activeSpells = new List<Spell>(); // this list stores the active spells in the player's inventory
    public List<Relic> activeRelics = new List<Relic>(); // this list stores the active relics in the player's inventory

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unit = GetComponent<Unit>();
        
        GameManager.Instance.player = gameObject;
    }

    public void StartLevel()
    {
        player_class = class_stats["mage"]; // TODO: Work with Michael to integrate UI elements to select class
        spellcaster = new SpellCaster(player_class.getMana(), player_class.getManaRegeneration(), player_class.getSpellpower(), Hittable.Team.PLAYER);
        StartCoroutine(spellcaster.ManaRegeneration());

        hp = new Hittable(RPN.calculateRPN(player_class.getHealth(), new Dictionary<string, int> { ["wave"] = GameManager.Instance.currentWave }), Hittable.Team.PLAYER, gameObject);  // Move this to class selection function
        hp.SetMaxHP(RPN.calculateRPN(player_class.getHealth(), new Dictionary<string, int> { ["wave"] = GameManager.Instance.currentWave })); // Replaces line above so that health updates correctly scaling with wave
        hp.OnDeath += Die;
        hp.team = Hittable.Team.PLAYER;

        unit.distance += RPN.calculateRPN(player_class.getSpeed(), new Dictionary<string, int> { ["wave"] = GameManager.Instance.currentWave });

        // tell UI elements what to show
        healthui.SetHealth(hp);
        manaui.SetSpellCaster(spellcaster);
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current[Key.Tab].wasPressedThisFrame)
        {
            spellCycle();
        }
    }
    

    void OnAttack(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || GameManager.Instance.state == GameManager.GameState.GAMEOVER) return;
        Vector2 mouseScreen = Mouse.current.position.value;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0;
        StartCoroutine(spellcaster.Cast(transform.position, mouseWorld));
    }

    void OnMove(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || GameManager.Instance.state == GameManager.GameState.GAMEOVER) return;
        unit.movement = value.Get<Vector2>() * speed;
    }

    void Die()
    {
        activeSpells.Clear();
        
        GameManager.Instance.state = GameManager.GameState.GAMEOVER;
    }

    public void ReadClassesJson()
    {
        var classtext = Resources.Load<TextAsset>("classes");

        JObject classes = JObject.Parse(classtext.text);

        foreach (var pair in classes)
        {
            var className = pair.Key;
            var classObj = pair.Value;

            string name = classObj["name"].ToString();
            int sprite = classObj["sprite"].ToObject<int>();
            string health = classObj["health"].ToString();
            string mana_regeneration = classObj["mana_regeneration"].ToString();
            string mana = classObj["mana"]?.ToString();
            string spellpower = classObj["spellpower"].ToString();
            string speed = classObj["speed"].ToString();

            var playerClass = new PlayerClass(name, sprite, health, mana_regeneration, mana, spellpower, speed);
            class_stats[className] = playerClass;
        }
    }

    void spellCycle()
{
    if (activeSpells.Count > 0)
    {
        int nextSpell = ((activeSpells.IndexOf(spellcaster.spell)) + 1) % activeSpells.Count;
        spellcaster.SetSpell(activeSpells[nextSpell]);
        spellui.SetSpell(spellcaster.spell);
        Image iconImage = activeSpell.GetComponent<Image>();
        GameManager.Instance.spellIconManager.PlaceSprite(activeSpells[nextSpell].GetIcon(), iconImage);
        
        

    }
    else return;
}
}
