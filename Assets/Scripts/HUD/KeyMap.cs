using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class KeyMap : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject controlMenu;
    public Button quit;
    
    public Button continuing;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(quit.gameObject);
        quit.onClick.AddListener(QuitToPause);
    }
    public void QuitToPause()
    {
        controlMenu.SetActive(false);
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(continuing.gameObject);
    }
}

