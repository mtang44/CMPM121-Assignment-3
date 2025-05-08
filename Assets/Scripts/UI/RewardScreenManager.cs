using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

public class RewardScreenManager : MonoBehaviour
{
    public GameObject rewardUI;
    public TMP_Text SpellDescription;
    public TMP_Text SpellName;

    public GameObject spellAcquiredTxt;
    public GameObject spellDeniedTxt;
    public GameObject icon;
    public GameObject DisplayIcon1;
    public GameObject DisplayIcon2;
    public GameObject DisplayIcon3;
    public GameObject DisplayIcon4;
    
    public Spell spell;
    public Spell newRewardSpell;
    public GameObject spell_1_drop;
    public GameObject spell_2_drop;
    public GameObject spell_3_drop;
    public GameObject spell_4_drop;
   
    public GameObject acquiredButton;
    public bool running = false;
    public List<GameObject> spellDisplayIcons = new List<GameObject>();
    
    public int counter = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.WAVEEND)
        {   
            rewardUI.SetActive(true);
            
            Debug.Log("Current state" + running);
            if(!running)
            {
                spellDeniedTxt.SetActive(false);
                spellAcquiredTxt.SetActive(false);
                acquiredButton.SetActive(true);
                running = true;
                
                // enemiesKilledLabel.text = "Enemies Killed: " + GameManager.Instance.numEnemiesKilled;
                // Then, in your method:
                display();
                
                newRewardSpell =  new SpellBuilder().MakeRandomSpell(GameManager.Instance.player.GetComponent<PlayerController>().spellcaster); 
                



                Debug.Log(newRewardSpell.description);
                SpellDescription.text = "Spell Description " + newRewardSpell.description;
                SpellName.text = "Spell Name " + newRewardSpell.name;
                GameManager.Instance.spellIconManager.PlaceSprite(newRewardSpell.GetIcon(), icon.GetComponent<Image>());
                // deactivates spell drop button if spell is 
                if(GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.Count < 4){
                spell_1_drop.SetActive(false);
                spell_2_drop.SetActive(false);
                spell_3_drop.SetActive(false);
                spell_4_drop.SetActive(false);
                }
            }
           
        }
        else
        {
            rewardUI.SetActive(false);
        }
    }
    // when button is pressed spell is stored in spell list, and updated to reward screen display
    public void gainSpell()
    {   
        if(GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.Count < 4){
            GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.Add(newRewardSpell);
            //  SpellUI rewardSpellUI = new SpellUI();
            // rewardSpellUI.SetSpell(newRewardSpell);
            // GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.Add(rewardSpellUI);
            spellAcquiredTxt.SetActive(true);
            acquiredButton.SetActive(false);
            display();
        }
        else{
            spellDeniedTxt.SetActive(true);

            spell_1_drop.SetActive(true);
            spell_2_drop.SetActive(true);
            spell_3_drop.SetActive(true);
            spell_4_drop.SetActive(true);
        }
    } 
    public void nextWave()
    {
        Debug.Log("Setting running to false");
        running = false;
    }

    public void dropSpell(int i)
    {
       GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.RemoveAt(i);
       display();
        spellDeniedTxt.SetActive(false);

    }
    
    public void display()
    {
        var activeSpells = GameManager.Instance.player.GetComponent<PlayerController>().activeSpells;
        for (int j = 0; j < Mathf.Min(activeSpells.Count, spellDisplayIcons.Count); j++)
        {
            Spell spell = activeSpells[j];
            Image iconImage = spellDisplayIcons[j].GetComponent<Image>();
            GameManager.Instance.spellIconManager.PlaceSprite(spell.GetIcon(), iconImage);
        }
    }
}

