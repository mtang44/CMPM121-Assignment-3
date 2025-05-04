using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public Image level_selector;
    public GameObject button;
    public GameObject enemy;
    public SpawnPoint[] SpawnPoints;
    private Dictionary <string, Enemy> enemies;
    private Dictionary <string, Level> levels;    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //==========================================================

    void Start()
    {
        GameObject selector = Instantiate(button, level_selector.transform);
        selector.transform.localPosition = new Vector3(0, 130);
        selector.GetComponent<MenuSelectorController>().spawner = this;
        selector.GetComponent<MenuSelectorController>().SetLevel("Start");
        enemies = readEnemiesJson();
        levels = readLevelsJson();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel(string levelname)
    {
        level_selector.gameObject.SetActive(false);
        // this is not nice: we should not have to be required to tell the player directly that the level is starting
<<<<<<< Updated upstream
        GameManager.Instance.player.GetComponent<PlayerController>().StartLevel();
        StartCoroutine(SpawnWave());
=======
        wave_count = 1;
        GameManager.Instance.currentWave = wave_count;
        Debug.Log(level_name);
        currentLevel = levels[level_name];
        GameManager.Instance.player.GetComponent<PlayerController>().StartLevel();
        StartCoroutine(SpawnWave());
        
    }

    public void NextWave() {
        wave_count++;
        GameManager.Instance.currentWave++;
        Debug.Log("wave: " + wave_count);
        StartCoroutine(SpawnWave());
    }
    IEnumerator SpawnWave()
    {
        GameManager.Instance.state = GameManager.GameState.COUNTDOWN;
        GameManager.Instance.countdown = 3;
        for (int i = 3; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            GameManager.Instance.countdown--;
        }
        GameManager.Instance.state = GameManager.GameState.INWAVE;
        foreach (Spawn item in currentLevel.spawns) {
          
            Spawn enemy_spawn = item;
            
            StartCoroutine(SpawnEnemyType(item));
        }
        
        yield return new WaitUntil(() => spawnsRunning == 0);
        yield return new WaitUntil(() => GameManager.Instance.enemy_count == 0);
        

        
        if(  GameManager.Instance.state != GameManager.GameState.GAMEOVER)
        {
            GameManager.Instance.state = GameManager.GameState.WAVEEND;
        }

        //      while n < count:
        //      required = next in sequence
        //     for i = 1 to required:
        //           SpawnEnemy(enemy type, Spawn attributes)
        //           n++
        //           if n == count: break
        //     yield return new WaitForSeconds(delay)
        // done
        // GameManager.Instance.state = GameManager.GameState.COUNTDOWN;
        // GameManager.Instance.countdown = 3;
        // for (int i = 3; i > 0; i--)
        // {
        //     yield return new WaitForSeconds(1);
        //     GameManager.Instance.countdown--;
        // }
        // GameManager.Instance.state = GameManager.GameState.INWAVE;
        // for (int i = 0; i < 10; ++i)
        // {
        //     yield return SpawnEnemy(toSpawn, spawnAttributes); // add spawn attributes 
        // }
        
    }

    IEnumerator SpawnEnemyType(Spawn waveSpawn) {
        spawnsRunning++;
        Enemy toSpawn = enemies[waveSpawn.enemy];
        Debug.Log("Name: " + toSpawn.name);
        Dictionary<string, int> spawnAttributes = new Dictionary<string, int>();
        int toSpawn_count = RPN.calculateRPN(waveSpawn.count, new Dictionary<string, int> {{"wave", wave_count}});
        Debug.Log("Count: " + toSpawn_count);
        spawnAttributes["hp"] = RPN.calculateRPN(waveSpawn.hp, new Dictionary<string, int> {{"wave", wave_count}, {"base", toSpawn.hp}});
        // toSpawn.location = enemies[waveSpawn.enemy].location;
        toSpawn.speed = enemies[waveSpawn.enemy].speed;
        toSpawn.damage = enemies[waveSpawn.enemy].damage;
        int numSpawned = 0;
        int sequenceIndex = 0;
        int required;
        
        while(numSpawned < toSpawn_count)
        {
            required = waveSpawn.sequence[sequenceIndex];
            if (required + numSpawned > toSpawn_count) {
                required = toSpawn_count - numSpawned;
            }
            for (int i = 0; i < required; i++) {
                StartCoroutine(SpawnEnemy(toSpawn, spawnAttributes));
                numSpawned++;
            }
            sequenceIndex++;
            if (sequenceIndex == waveSpawn.sequence.Count) {
                sequenceIndex = 0;
            }
           
            yield return new WaitForSeconds(waveSpawn.delay);
        }
        spawnsRunning--;
    }

    IEnumerator SpawnEnemy(Enemy toSpawn, Dictionary<string, int> spawnAttributes)
    {
        SpawnPoint spawn_point = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Vector2 offset = Random.insideUnitCircle * 1.8f;
                
        Vector3 initial_position = spawn_point.transform.position + new Vector3(offset.x, offset.y, 0);
        GameObject new_enemy = Instantiate(enemy, initial_position, Quaternion.identity);

        new_enemy.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.enemySpriteManager.Get(toSpawn.sprite);
        EnemyController en = new_enemy.GetComponent<EnemyController>();
        Debug.Log("with hp of: " + spawnAttributes["hp"]);
        en.hp = new Hittable(spawnAttributes.ContainsKey("hp") ? spawnAttributes["hp"] : toSpawn.hp, Hittable.Team.MONSTERS, new_enemy);
        en.speed = spawnAttributes.ContainsKey("speed") ? spawnAttributes["speed"] : toSpawn.speed;
        
        GameManager.Instance.AddEnemy(new_enemy);
        yield return new WaitForSeconds(0.5f);
        /* SpawnPoint spawn_point = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Vector2 offset = Random.insideUnitCircle * 1.8f;        
        Vector3 initial_position = spawn_point.transform.position + new Vector3(offset.x, offset.y, 0);
        // Create Instance
        GameObject new_enemy = Instantiate(enemy, initial_position, Quaternion.identity);
        // Set Parameters; you will need to replace the numbers with the evaluated RPN values
        new_enemy.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.enemySpriteManager.Get(0);
        EnemyController en = new_enemy.GetComponent<EnemyController>();
        en.hp = new Hittable(50, Hittable.Team.MONSTERS, new_enemy);
        en.speed = 10;
        GameManager.Instance.AddEnemy(new_enemy);*/
>>>>>>> Stashed changes
    }

    //==================================================
    public Dictionary<string, Enemy> readEnemiesJson()
    {
        Dictionary<string, Enemy> enemy_types = new Dictionary<string, Enemy>();
        var enemytext = Resources.Load<TextAsset>("enemies");
        
        JToken jo = JToken.Parse(enemytext.text);
        foreach (var enemy in jo)
        {
            Enemy en = enemy.ToObject<Enemy>();// request construction of object NEED Enemy class first
            enemy_types[en.name] = en;
        }
        return enemy_types;
    }
     public Dictionary<string, Level >readLevelsJson()
     {
        Dictionary<string, Level> level_types = new Dictionary<string, Level>();
        var leveltext = Resources.Load<TextAsset>("levels");
        
        JToken jo = JToken.Parse(leveltext.text);
        foreach (var level in jo)
        {
            Level le = level.ToObject<Level>();// request construction of object NEED Enemy class first
            level_types[le.name] = le;
        }
        return level_types;
     }

    public void NextWave()
    {
        StartCoroutine(SpawnWave());
    }


    IEnumerator SpawnWave()
    {
        Enemy toSpawn = new Enemy(); //ERASE THIS
        GameManager.Instance.state = GameManager.GameState.COUNTDOWN;
        GameManager.Instance.countdown = 3;
        for (int i = 3; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            GameManager.Instance.countdown--;
        }
        GameManager.Instance.state = GameManager.GameState.INWAVE;
        for (int i = 0; i < 10; ++i)
        {
            yield return SpawnZombie(toSpawn);
        }
        yield return new WaitWhile(() => GameManager.Instance.enemy_count > 0);
        GameManager.Instance.state = GameManager.GameState.WAVEEND;
    }

    IEnumerator SpawnZombie(Enemy toSpawn)
    {
        SpawnPoint spawn_point = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Vector2 offset = Random.insideUnitCircle * 1.8f;

        Vector3 initial_position = spawn_point.transform.position + new Vector3(offset.x, offset.y, 0);
        GameObject new_enemy = Instantiate(enemy, initial_position, Quaternion.identity);

        new_enemy.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.enemySpriteManager.Get(0);
        EnemyController en = new_enemy.GetComponent<EnemyController>();
        en.hp = new Hittable(toSpawn.hp, Hittable.Team.MONSTERS, new_enemy);
        en.speed = 10;
        GameManager.Instance.AddEnemy(new_enemy);
        yield return new WaitForSeconds(0.5f);
    }

}
