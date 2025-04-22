using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardScreenManager : MonoBehaviour
{
    public GameObject rewardUI;
    TextMeshProUGUI enemiesKilledLabel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemiesKilledLabel = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GameManager.Instance.numEnemiesKilled);
        if (GameManager.Instance.state == GameManager.GameState.WAVEEND)
        {
            
            enemiesKilledLabel.text = "Enemies left: " + GameManager.Instance.numEnemiesKilled;
            rewardUI.SetActive(true);
        }
        else
        {
            rewardUI.SetActive(false);
        }
    }
}

