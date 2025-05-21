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
    public TMP_Text RelicDescription1;
    public TMP_Text RelicDescription2;
    public TMP_Text RelicDescription3;
    

    public GameObject relicUIDisplay;
    public GameObject spellAcquiredTxt;
    public GameObject spellDeniedTxt;
    public GameObject icon;
    public GameObject relicAcquiredTxt;

    public GameObject spell_1_drop;
    public GameObject spell_2_drop;
    public GameObject spell_3_drop;
    public GameObject spell_4_drop;

    public GameObject relic_1_take;
    public GameObject relic_2_take;
    public GameObject relic_3_take;
    public GameObject acquiredButton;

    public GameObject RelicDisplayIcon1;
    public GameObject RelicDisplayIcon2;
    public GameObject RelicDisplayIcon3;

    public Spell spell;
    public Spell newRewardSpell;
    
    public Relic newRewardRelic1;
    public Relic newRewardRelic2;
    public Relic newRewardRelic3;

    public List<GameObject> spellDisplayIcons = new List<GameObject>();
    //public List<GameObject> relicDisplayIcons = new List<GameObject>(); might need later

    public string spellname;
    public string spelldescription;
    public bool running = false;
    
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
            if(!running)
            {
                // turns off/on certain UI game Objects
                running = true; // makes it so that game does not keep regenerating new spells every update
                adjustUIElements();
                display(); // updates spell inventory display on reward screen.
                spellDisplay(); // updates and creates a new spell to display
                if(GameManager.Instance.currentWave % 1 == 0) // every thid wave spawn relics // change back to 3, set to 1 for testing
                {
                    relicDisplay();
                }
                else{ 
                    relicUIDisplay.SetActive(false);
                }
            }
        }
        else
        {
            rewardUI.SetActive(false);
        }
    }
    public void adjustUIElements()
    {
        spellDeniedTxt.SetActive(false);
        spellAcquiredTxt.SetActive(false);
        acquiredButton.SetActive(true);
        relicAcquiredTxt.SetActive(false);
    }
    // when button is pressed spell is stored in spell list, and updated to reward screen display
    public void gainSpell()
    {
        if (GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.Count < 4)
        {
            GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.Add(newRewardSpell);
            //  SpellUI rewardSpellUI = new SpellUI();
            // rewardSpellUI.SetSpell(newRewardSpell);
            // GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.Add(rewardSpellUI);
            spellAcquiredTxt.SetActive(true);
            acquiredButton.SetActive(false);
            spellname = "";
            spelldescription = "";
            display();

        }
        else
        {
            spellDeniedTxt.SetActive(true);

            spell_1_drop.SetActive(true);
            spell_2_drop.SetActive(true);
            spell_3_drop.SetActive(true);
            spell_4_drop.SetActive(true);
        }
    } 
    public void nextWave()
    {
        running = false;
    }

    public void dropSpell(int i)
    {
       GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.RemoveAt(i);
   
        spellDeniedTxt.SetActive(false);
        spell_1_drop.SetActive(false);
        spell_2_drop.SetActive(false);
        spell_3_drop.SetActive(false);
        spell_4_drop.SetActive(false);
        display();

    }
    // updates spell inventory UI display
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
     // create new spell and displays it to Reward Screen UI
    public void spellDisplay()
    {
        newRewardSpell =  new SpellBuilder().MakeRandomSpell(GameManager.Instance.player.GetComponent<PlayerController>().spellcaster); 
        // gets spell's name:
        Spell printSpell = newRewardSpell;
        if(printSpell is Spell)
        {
                spellname += printSpell.GetName() ;
        }
        else{
            while (printSpell is ModifierSpell modifierSpell) {
            spellname = modifierSpell.GetName() + " ";
            printSpell = modifierSpell.GetChild();
        }
        }
        // get spell's description
        printSpell = newRewardSpell;
        while (printSpell is ModifierSpell modifierSpell) {
            spelldescription += modifierSpell.GetName() + ": " + modifierSpell.GetDescription() + "\n";
            printSpell = modifierSpell.child;
        }
        spelldescription += printSpell.GetName() + ": " + printSpell.GetDescription() + "\n";

        SpellDescription.text = "Spell Description: " + spelldescription;
        SpellName.text = "Spell Name: " + spellname;
        GameManager.Instance.spellIconManager.PlaceSprite(newRewardSpell.GetIcon(), icon.GetComponent<Image>());

        // deactivates spell drop button if spell count < 4 
        if(GameManager.Instance.player.GetComponent<PlayerController>().activeSpells.Count < 4){
        spell_1_drop.SetActive(false);
        spell_2_drop.SetActive(false);
        spell_3_drop.SetActive(false);
        spell_4_drop.SetActive(false);
        }
    }
    // gets three new relics and displays the to the screen. 
    public void relicDisplay()
    {  
        relic_1_take.SetActive(true);
        relic_2_take.SetActive(true);
        relic_3_take.SetActive(true);

        Image iconImage;
        relicUIDisplay.SetActive(true);

        // creates / displays new relic in slot 1
        newRewardRelic1 = new RelicBuilder().MakeRandomRelic(GameManager.Instance.player);
        RelicDescription1.text = newRewardRelic1.GetDescription();
        iconImage = RelicDisplayIcon1.GetComponent<Image>();
        GameManager.Instance.relicIconManager.PlaceSprite(newRewardRelic1.GetSprite(), iconImage);
        

        // creates / displays new relic in slot 2
        newRewardRelic2 = new RelicBuilder().MakeRandomRelic(GameManager.Instance.player);
        RelicDescription2.text = newRewardRelic2.GetDescription();
        iconImage = RelicDisplayIcon2.GetComponent<Image>();
        GameManager.Instance.relicIconManager.PlaceSprite(newRewardRelic2.GetSprite(), iconImage);
         


        // creates / displays new relic in slot 3
        newRewardRelic3 = new RelicBuilder().MakeRandomRelic(GameManager.Instance.player);
        RelicDescription3.text = newRewardRelic3.GetDescription();
        iconImage = RelicDisplayIcon3.GetComponent<Image>();
        GameManager.Instance.relicIconManager.PlaceSprite(newRewardRelic3.GetSprite(), iconImage);
 
        

      
    }
    // when a relic is taken, it's button assigns that relic to the player's relic inventory
    public void acceptRelic(int index)
    {
        if (index == 1)
        {
            GameManager.Instance.player.GetComponent<PlayerController>().activeRelics.Add(newRewardRelic1);
            EventBus.Instance.DoRelicPickup(newRewardRelic1);
        }
        if (index == 2)
        {
            GameManager.Instance.player.GetComponent<PlayerController>().activeRelics.Add(newRewardRelic2);
            EventBus.Instance.DoRelicPickup(newRewardRelic2);
        }
        if (index == 3)
        {
            GameManager.Instance.player.GetComponent<PlayerController>().activeRelics.Add(newRewardRelic3);
            EventBus.Instance.DoRelicPickup(newRewardRelic3);
        }
        // on relic accept, UI for buttons disabled.
        relic_1_take.SetActive(false);
        relic_2_take.SetActive(false);
        relic_3_take.SetActive(false);
        relicAcquiredTxt.SetActive(true);

    }

}

