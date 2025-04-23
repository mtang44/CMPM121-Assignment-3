using UnityEngine;
using TMPro;

public class StatsLabelController : MonoBehaviour
{
    public TMP_Text enemiesKilledLabel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       if (GameManager.Instance.state == GameManager.GameState.WAVEEND)
        {   
            enemiesKilledLabel.text = "Enemies Killed: " + GameManager.Instance.numEnemiesKilled;
        }
    }
}
