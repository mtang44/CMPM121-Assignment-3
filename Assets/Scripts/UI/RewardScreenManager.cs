using UnityEngine;

public class RewardScreenManager : MonoBehaviour
{
    public GameObject rewardUI;
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
<<<<<<< Updated upstream
=======
           // enemiesKilledLabel.text = "Enemies Killed: " + GameManager.Instance.numEnemiesKilled;
            //    Spell newRewardSpell = MakeRandomSpell();
>>>>>>> Stashed changes
        }
        else
        {
            rewardUI.SetActive(false);
        }
    }
}
