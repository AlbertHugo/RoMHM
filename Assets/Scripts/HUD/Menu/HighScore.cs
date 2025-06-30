using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HighScore : MonoBehaviour
{
    public Button quitScore;
    public GameObject mainMenu;

    public TextMeshProUGUI highestScore; 
    public Button start;
    void Start()
    {
        quitScore.onClick.AddListener(QuitTheScore);
        highestScore.text = "HIGH SCORE:\n" + GlobalVariables.highScore.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.L) || Input.GetMouseButtonDown(1))
        {
            var selected = EventSystem.current.currentSelectedGameObject;
            if (selected != null)
            {
                var button = selected.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.Invoke();
                }
            }
        }
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(quitScore.gameObject);
        }
    }
    void QuitTheScore()
    {
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(start.gameObject);
        gameObject.SetActive(false);
    }
}
