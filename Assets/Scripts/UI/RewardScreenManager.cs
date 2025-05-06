using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardScreenManager : MonoBehaviour
{
    public GameObject rewardUI;
    public TMP_Text SpellDescription;
    public TMP_Text SpellName;
    public GameObject icon;
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
            //   ;
            newRewardSpell =  new SpellBuilder().MakeRandomSpell(GameManager.Instance.player.GetComponent<SpellCaster>());   
            SpellDescription.text = "Spell Description " + newRewardSpell.description;
            SpellName.text = "Spell Name " + newRewardSpell.name;
            GameManager.Instance.spellIconManager.PlaceSprite(newRewardSpell.GetIcon(), icon.GetComponent<Image>());
            

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

    public void dropSpell()
    {
       
        //GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.Remove(removed);
            // figure out drop specific spell. 
    }
}

