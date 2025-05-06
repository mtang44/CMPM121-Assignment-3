using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardScreenManager : MonoBehaviour
{
    public GameObject rewardUI;
    public TMP_Text SpellDescription;
    public TMP_Text SpellName;
    public GameObject icon;
    public Spell spell;
    public Spell newRewardSpell;
    public GameObject spell_1_drop;
    public GameObject spell_2_drop;
    public GameObject spell_3_drop;
    public GameObject spell_4_drop;
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
           // enemiesKilledLabel.text = "Enemies Killed: " + GameManager.Instance.numEnemiesKilled;
            foreach(var s in GameManager.Instance.player.GetComponent<PlayerController>().activeSpells)
            {
                GameManager.Instance.player.GetComponent<SpellUI>().SetSpell(s);
            }
            newRewardSpell =  new SpellBuilder().MakeRandomSpell(GameManager.Instance.player.GetComponent<SpellCaster>());   
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
        else
        {
            rewardUI.SetActive(false);
        }
    }
    public void gainSpell()
    {   
        if(GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.Count < 4){
            GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.Add(newRewardSpell);
        }
        else{

            spell_1_drop.SetActive(true);
            spell_2_drop.SetActive(true);
            spell_3_drop.SetActive(true);
            spell_4_drop.SetActive(true);
        }
    } 

    public void dropSpell(int i)
    {
       GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.RemoveAt(i);
        //GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.Remove(removed);
            // figure out drop specific spell. 
    }
    
}

