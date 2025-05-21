using UnityEngine;
using TMPro;

public class MenuSelectorController : MonoBehaviour
{
    public TextMeshProUGUI label;
    public string level; 
    public EnemySpawner spawner;
    public string my_class_name;
    public int my_class_index;
    public ClassSelectorManager class_manager;
 

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevel(string text)
    {
        level = text;
        label.text = text;
    }
    public void SetClass(string text, int index)
    {
        my_class_name = text;
        label.text = text;
        my_class_index = index;

    }
    public void SelectedClass()
    {
        Debug.Log("Starting level selection");
        GameManager.Instance.player.GetComponent<PlayerController>().player_class = GameManager.Instance.player.GetComponent<PlayerController>().player_classes[my_class_index];
        GameManager.Instance.state = GameManager.GameState.LEVELSELECT;
    }

    public void StartLevel()
    {
        spawner.StartLevel(level);
    }
}
   
