using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Hittable hp;
    public HealthBar healthui;
    public ManaBar manaui;

    public string class_name;
    public JObject class_stats;

    public SpellCaster spellcaster;
    public SpellUI spellui;

    public int speed;

    public Unit unit;

    public List<Spell> activeSpells = new List<Spell>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unit = GetComponent<Unit>();
        GameManager.Instance.player = gameObject;

    }

    public void StartLevel()
    {
        class_stats = ReadClassesJson("mage");
        string currentMana = RPN.calculateRPN(class_stats["mana"].ToString(), new Dictionary<string, int> { ["wave"] = GameManager.Instance.currentWave } ).ToString();
        string currentManaReg = RPN.calculateRPN(class_stats["mana_regeneration"].ToString(), new Dictionary<string, int> { ["wave"] = GameManager.Instance.currentWave }).ToString();
        string currentSpellPower = RPN.calculateRPN(class_stats["spellpower"].ToString(), new Dictionary<string, int> { ["wave"] = GameManager.Instance.currentWave }).ToString();
        spellcaster = new SpellCaster(currentMana, currentManaReg, currentSpellPower, Hittable.Team.PLAYER);
        StartCoroutine(spellcaster.ManaRegeneration());


        int currentHealth = RPN.calculateRPN(class_stats["health"].ToString(), new Dictionary<string, int> { ["wave"] = GameManager.Instance.currentWave });
        hp = new Hittable(currentHealth, Hittable.Team.PLAYER, gameObject);
        hp.OnDeath += Die;
        hp.team = Hittable.Team.PLAYER;

        // tell UI elements what to show
        healthui.SetHealth(hp);
        manaui.SetSpellCaster(spellcaster);
        spellui.SetSpell(spellcaster.spell);
    }

    // Update is called once per frame
    void Update()
    {

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
        GameManager.Instance.state = GameManager.GameState.GAMEOVER;
    }

    public JObject ReadClassesJson(string class_name)
    {
        var classtext = Resources.Load<TextAsset>("classes");
        JObject classes = JObject.Parse(classtext.text);
        return (JObject)classes[class_name];

    }

}
