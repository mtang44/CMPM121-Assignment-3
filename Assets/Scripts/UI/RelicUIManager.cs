using System.Collections.Generic;
using UnityEngine;

public class RelicUIManager : MonoBehaviour
{
    public GameObject relicUIPrefab;
    public PlayerController player;
    public List<GameObject> createdRUIs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBus.Instance.OnRelicPickup += OnRelicPickup;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.GAMEOVER)
        {
            foreach(var u in createdRUIs)
            {
                u.SetActive(false);
                Destroy(u);
            }
            createdRUIs.Clear();
        
        }
    }

    public void OnRelicPickup(Relic r)
    {
        // make a new Relic UI representation
        GameObject rui = Instantiate(relicUIPrefab, transform);
        createdRUIs.Add(rui);
        rui.transform.localPosition = new Vector3(-450 + 50 * (player.activeRelics.Count - 1), 0, 0);
        RelicUI ruic = rui.GetComponent<RelicUI>();
        ruic.transform.localScale = new Vector3(1.5f, 1.5f, 0);
        ruic.player = player;
        ruic.index = player.activeRelics.Count - 1;
        
    }
}
