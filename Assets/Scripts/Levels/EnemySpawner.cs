using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using NUnit.Framework.Constraints;

public class EnemySpawner : MonoBehaviour
{
    public Image level_selector;
    public GameObject button;
    public GameObject enemy;

    public SpawnPoint[] SpawnPoints;
    private Dictionary <string, Enemy> enemies;
    private Dictionary <string, Level> levels; 
    public RPN rpn;   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject selector = Instantiate(button, level_selector.transform);
        selector.transform.localPosition = new Vector3(0, 130);
        selector.GetComponent<MenuSelectorController>().spawner = this;
        selector.GetComponent<MenuSelectorController>().SetLevel("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel(string levelname)
    {
        level_selector.gameObject.SetActive(false);
        // this is not nice: we should not have to be required to tell the player directly that the level is starting
        GameManager.Instance.player.GetComponent<PlayerController>().StartLevel();
        foreach (Spawn enemy_spawn in levels[levelname].spawns) {
            for (int i = 0; i < levels[levelname].waves; i++) {
                StartCoroutine(SpawnWave(enemy_spawn, i));
            }
        }
        
    }

    public void NextWave()
    {
        StartCoroutine(SpawnWave()); // to change later
    }


    IEnumerator SpawnWave(Spawn waveSpawn, int currentWave)
    {
        Enemy toSpawn = enemies[waveSpawn.enemy];
        Dictionary<string, int> spawnAttributes = new Dictionary<string, int>();
        int toSpawn_count = rpn.calculateRPN(waveSpawn.count, new Dictionary<string, int> {{"wave", currentWave}});
        spawnAttributes["hp"] = rpn.calculateRPN(waveSpawn.hp, new Dictionary<string, int> {{"wave", currentWave}, {"base", toSpawn.hp}});
        toSpawn.speed = enemies[waveSpawn.enemy].speed;
        toSpawn.damage = enemies[waveSpawn.enemy].damage;
        int numSpawned = 0;
        int sequenceIndex = 0;
        int required;
       
        while(numSpawned < toSpawn_count)
        {
          if (waveSpawn.sequence.Any()) {
            required = waveSpawn.sequence[sequenceIndex];
            if (required + numSpawned > toSpawn_count) {
                required = toSpawn_count - numSpawned;
            } else {
                
            }
            for (int i = 0; i < required; i++) {
                StartCoroutine(SpawnEnemy(toSpawn, spawnAttributes));
                numSpawned++;
            }
            sequenceIndex++;
            if (sequenceIndex == waveSpawn.sequence.Count) {
                sequenceIndex = 0;
            }
          } else {
            StartCoroutine(SpawnEnemy(toSpawn, spawnAttributes));
            numSpawned++;
          }
           
          
          yield return new WaitForSeconds(waveSpawn.delay);
        }

//      while n < count:
//      required = next in sequence
//     for i = 1 to required:
//           SpawnEnemy(enemy type, Spawn attributes)
//           n++
//           if n == count: break
//     yield return new WaitForSeconds(delay)
// done
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
            yield return SpawnEnemy(toSpawn); // add spawn attributes 
        }
        yield return new WaitWhile(() => GameManager.Instance.enemy_count > 0);
        GameManager.Instance.state = GameManager.GameState.WAVEEND;
    }

    IEnumerator SpawnEnemy(Enemy toSpawn, Dictionary<string, int> spawnAttributes)
    {
        SpawnPoint spawn_point = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Vector2 offset = Random.insideUnitCircle * 1.8f;
                
        Vector3 initial_position = spawn_point.transform.position + new Vector3(offset.x, offset.y, 0);
        GameObject new_enemy = Instantiate(enemy, initial_position, Quaternion.identity);

        new_enemy.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.enemySpriteManager.Get(0);
        EnemyController en = new_enemy.GetComponent<EnemyController>();

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
    }
}
