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
        if (GameManager.Instance.state != GameManager.GameState.GAMEOVER && GameManager.Instance.state != GameManager.GameState.PREGAME && GameManager.Instance.state != GameManager.GameState.LEVELSELECT)
        {
            if (index >= 0)
            {
                Debug.Log("Index = " + index);
                // Relics could have labels and/or an active-status
                
                Relic r = player.activeRelics[index];
                GameManager.Instance.relicIconManager.PlaceSprite(r.sprite, icon);
                //label.text = r.GetName();
                //label.transform.localScale = new Vector3(0, 2, 0);
                label.fontSize = 8;
                highlight.SetActive(r.IsActive());
            }
            
        
        }

    }
}
