using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Hittable hp;
    public HealthBar healthui;
    public ManaBar manaui;

    public PlayerClass player_class;
    public List<PlayerClass> player_classes = new List<PlayerClass>();

    public SpellCaster spellcaster;
    public SpellUI spellui; // need a list of spellUI
    public SpellUIContainer spellContainer;
    public GameObject activeSpell;
    public RewardScreenManager rewardscreen;


    public SpriteRenderer sprite;
    public Unit unit;


    // Relic shit. God help us.
    private Coroutine vampireCoroutine = null;
    public float speedMult = 1f;
    public bool vampire = false;
    public int baseDamage = 0;

    // public List<SpellUI> activeSpellsUI = new List<SpellUI>();// stores spell for UI display while playing

    public List<Spell> activeSpells = new List<Spell>(); // this list stores the active spells in the player's inventory
    public List<Relic> activeRelics = new List<Relic>(); // this list stores the active relics in the player's inventory

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        unit = GetComponent<Unit>();
        ReadClassesJson();
        GameManager.Instance.player = gameObject;
    }

    public void StartLevel()
    {
        sprite.sprite = GameManager.Instance.playerSpriteManager.Get(player_class.getSprite());
        //player_class = player_classes[0]; // TODO: Work with Michael to integrate UI elements to select class
        spellcaster = new SpellCaster(player_class.getMana(), player_class.getManaRegeneration(), player_class.getSpellpower(), Hittable.Team.PLAYER);
        StartCoroutine(spellcaster.ManaRegeneration());
        if (GameManager.Instance.currentWave <= 1)
        {
            hp = new Hittable(RPN.calculateRPN(player_class.getHealth(), new Dictionary<string, int> { ["wave"] = GameManager.Instance.currentWave }), Hittable.Team.PLAYER, gameObject);  // Move this to class selection function
        }
        else
        {
            hp.SetMaxHP(RPN.calculateRPN(player_class.getHealth(), new Dictionary<string, int> { ["wave"] = GameManager.Instance.currentWave }));
        }
        hp.OnDeath += Die;
        hp.team = Hittable.Team.PLAYER;

        // tell UI elements what to show
        healthui.SetHealth(hp);
        manaui.SetSpellCaster(spellcaster);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.Tab].wasPressedThisFrame)
        {
            spellCycle();
        }
    }


    void OnAttack(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || GameManager.Instance.state == GameManager.GameState.GAMEOVER || GameManager.Instance.state == GameManager.GameState.LEVELSELECT) return;
        Vector2 mouseScreen = Mouse.current.position.value;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0;

        StartCoroutine(spellcaster.Cast(transform.position, mouseWorld));
    }

    void OnMove(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || GameManager.Instance.state == GameManager.GameState.GAMEOVER) return;

        Vector2 input = value.Get<Vector2>();
        float inputMagnitude = input.magnitude;
        if (inputMagnitude > Mathf.Epsilon)
        {
            EventBus.Instance.DoMove(transform.position, hp);
        }
        float speed = RPN.calculateRPN(player_class.getSpeed(), new Dictionary<string, int> { ["wave"] = GameManager.Instance.currentWave }) * speedMult;

        // Add speed to the magnitude, then apply direction
        float newMagnitude = inputMagnitude + speed;
        unit.movement = input.normalized * newMagnitude;
    }

    void Die()
    {
        vampire = false;
        activeSpells.Clear();
        activeRelics.Clear();
        GameManager.Instance.state = GameManager.GameState.GAMEOVER;
    }

    public void ReadClassesJson()
    {
        var classtext = Resources.Load<TextAsset>("classes");
        JObject jo = JObject.Parse(classtext.text);

        player_classes.Clear();

        foreach (var prop in jo.Properties())
        {
            string name = prop.Name;
            JObject data = (JObject)prop.Value;

            int sprite = data["sprite"] != null ? data["sprite"].ToObject<int>() : 0;
            string health = data["health"] != null ? data["health"].ToString() : "";
            string mana_regeneration = data["mana_regeneration"] != null ? data["mana_regeneration"].ToString() : "";
            string mana = data["mana"] != null ? data["mana"].ToString() : "";
            string spellpower = data["spellpower"] != null ? data["spellpower"].ToString() : "";
            string speed = data["speed"] != null ? data["speed"].ToString() : "";

            PlayerClass pcc = new PlayerClass(
                name,
                sprite,
                health,
                mana_regeneration,
                mana,
                spellpower,
                speed
            );
            player_classes.Add(pcc);
        }

        //Debug.Log("Classes loaded: " + player_classes.Count);
        //foreach (var pcc in player_classes)
        //{
        //Debug.Log($"Class: {pcc.getName()}, Speed: {pcc.getSpeed()}, Spellpower: {pcc.getSpellpower()}");
        //}
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

    public void EnableVampire()
    {
        if (!vampire)
        {
            vampire = true;
            if (vampireCoroutine == null)
                vampireCoroutine = StartCoroutine(VampireDamageRoutine());
        }
    }
    private IEnumerator VampireDamageRoutine()
    {
        while (vampire)
        {
            if (GameManager.Instance.state == GameManager.GameState.INWAVE) // So the player doesn't just randomly die in between waves
            {
                int dmg = 2 * GameManager.Instance.currentWave;
                var damage = new Damage(dmg, Damage.Type.FIRE);
                hp.Damage(damage);
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
