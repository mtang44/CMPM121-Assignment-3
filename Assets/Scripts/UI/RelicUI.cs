using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RelicUI : MonoBehaviour
{
    public PlayerController player;
    public int index;

    public Image icon;
    public GameObject highlight;
    public TextMeshProUGUI label;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // if a player has relics, this is how you *could* show them
        
        
        
    }

    // Update is called once per frame
    void Update()
    {

        // Relics could have labels and/or an active-status
        Debug.Log("index :" + index);
        Relic r = player.activeRelics[index];
        GameManager.Instance.relicIconManager.PlaceSprite(r.sprite, icon);
        label.fontSize = 10;
        label.text = r.GetName();
        label.transform.localScale = new Vector3(1.5f, 2, 0);
        //highlight.SetActive(r.IsActive());

    }
}
