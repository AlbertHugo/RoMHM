using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheatPanelCode : MonoBehaviour
{
    public Button start;
    public Button phase1;
    public Button phase2;
    public Button boss1;
    public Button phase3;
    public Button boss2;
    public Button boss3;
    public Button phase4;
    public Button quit;
    public GameObject menu;
    void Start()
    {
        phase1.onClick.AddListener(LoadPhase1);
        phase2.onClick.AddListener(LoadPhase2);
        boss1.onClick.AddListener(LoadBoss1);
        phase3.onClick.AddListener(LoadPhase3);
        boss2.onClick.AddListener(LoadBoss2);
        boss3.onClick.AddListener(LoadBoss3);
        phase4.onClick.AddListener(LoadPhase4);
        quit.onClick.AddListener(GoToMenu);
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
    void LoadPhase1()
    {
        SceneManager.LoadScene("Phase1");
    }
    void LoadPhase2()
    {
        SceneManager.LoadScene("Phase2");
    }
    void LoadBoss1()
    {
        SceneManager.LoadScene("Boss1");
    }
    void LoadPhase3()
    {
        SceneManager.LoadScene("Phase3");
    }
    void LoadBoss2()
    {
        SceneManager.LoadScene("Boss2");
    }
    void LoadBoss3()
    {
        SceneManager.LoadScene("Boss3");
    }
    void LoadPhase4()
    {
        SceneManager.LoadScene("Phase4");
    }
    void GoToMenu()
    {
        menu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(start.gameObject);
        gameObject.SetActive(false);
    }
}
