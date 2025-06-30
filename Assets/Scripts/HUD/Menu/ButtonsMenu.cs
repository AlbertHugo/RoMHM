using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonsMenu : MonoBehaviour
{
    public Button start;
    public Button tutorial;
    public Button quitGame;
    public Button cheat;
    public Button score;
    public Button phase1;
    public Button quitScore;
    public GameObject cheatPanel;
    public GameObject scorePanel;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(start.gameObject);
        GlobalVariables.highScore = PlayerPrefs.GetInt("HighScore", 0);
        start.onClick.AddListener(GameStart);
        tutorial.onClick.AddListener(Tutorial);
        quitGame.onClick.AddListener(QuitTheGame);
        cheat.onClick.AddListener(CheatCode);
        score.onClick.AddListener(ShowScore);
        GlobalVariables.isSelecting = true;
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
            EventSystem.current.SetSelectedGameObject(start.gameObject);
        }
    }

    void GameStart()
    {
        SceneManager.LoadScene("HQ");
    }

    void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    void QuitTheGame()
    {
        Application.Quit();
    }

    void CheatCode()
    {
        cheatPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(phase1.gameObject);
        gameObject.SetActive(false);
    }
    void ShowScore()
    {
        scorePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(quitScore.gameObject);
        gameObject.SetActive(false);
    }
}
