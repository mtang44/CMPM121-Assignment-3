using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
public class SpellUIContainer : MonoBehaviour
{
    public GameObject[] spellUIs;
    public PlayerController player;
    public List<GameObject> spellDisplayIcons = new List<GameObject>();
    public bool alreadyRunning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // we only have one spell (right now)

            spellUIs[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // updates inventory icons 
        updateDisplay();
        if(GameManager.Instance.state == GameManager.GameState.GAMEOVER )
        {
            resetDisplay();
        }


        
    }
    public void updateDisplay()
    {
        var activeSpells = GameManager.Instance.player.GetComponent<PlayerController>().activeSpells;
       for (int j = 0; j < Mathf.Min(activeSpells.Count, spellDisplayIcons.Count); j++)
        {
            spellUIs[j].SetActive(true);
            Spell spell = activeSpells[j];
        Image iconImage = spellDisplayIcons[j].GetComponent<Image>();
           GameManager.Instance.spellIconManager.PlaceSprite(spell.GetIcon(), iconImage);
        }
        
    }
    public void resetDisplay()
    {
        var activeSpells = GameManager.Instance.player.GetComponent<PlayerController>().activeSpells;
       for (int j = 0; j < Mathf.Min(spellUIs.Length, spellDisplayIcons.Count); j++)
        {
            spellUIs[j].SetActive(false);
        }
    }
    // have a function here that calls spell: setSpell with the spell we are adding. 

}
