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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // we only have one spell (right now)
        for(int i = 0; i< spellUIs.Length; ++i)
        {
            spellUIs[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    // var activeSpells = GameManager.Instance.player.GetComponent<PlayerController>().activeSpells;
    //    for (int j = 0; j < Mathf.Min(activeSpells.Count, spellUIs.Length); j++)
    //     {
    //         Spell spell = activeSpells[j];
    //         Image iconImage = spellUIs[j].GetComponent<Image>();
    //         GameManager.Instance.spellIconManager.PlaceSprite(spell.GetIcon(), iconImage);
    //     }
    }
    // have a function here that calls spell: setSpell with the spell we are adding. 

}
