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
    public 
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
      GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.Add(newRewardSpell);

        // takes in player and spell and stores it in a group of other spells. 
        // this 

    } 

    public void dropSpell()
    {
            // figure out drop specific spell. 
    }
}

