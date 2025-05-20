using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class ClassSelectorManager : MonoBehaviour
{
    public Image class_selector;
    public GameObject button;
    public GameObject classSelectorUI;
    private List<GameObject> classButtons = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < GameManager.Instance.player.GetComponent<PlayerController>().player_classes.Count; i++)
        {
            GameObject class_button = new GameObject();
            class_button = Instantiate(button, class_selector.transform);
            class_button.transform.localPosition = new Vector3(0, 100 + (40 * i));
            class_button.GetComponent<MenuSelectorController>().SetClass(GameManager.Instance.player.GetComponent<PlayerController>().player_classes[i].getName(), i);
            classButtons.Add(class_button);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME)
        {
            classSelectorUI.SetActive(true);
        }
        else
        {
           classSelectorUI.SetActive(false);
        }
    }
    public void LoadClassSelector()
    {
        GameManager.Instance.state = GameManager.GameState.PREGAME;
    }
}
