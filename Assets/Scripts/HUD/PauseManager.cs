using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Slider masterSlider;
    public GameObject configMenuUI;
    public Button continuing;
    public Button config;
    public Button quit;
    private bool isPaused = false;
    public Animator animator1;
    public Animator animator2;
    public Animator animator3;
    public Animator quitConfig;
    public Animator config1;
    public Animator config2;
    public Animator config3;
    public Animator controlAnimator;
    public Animator quitControls;
    public Button controls;
    public GameObject controlsMenu;

    private void Start()
    {
        animator1.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator2.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator3.updateMode = AnimatorUpdateMode.UnscaledTime;
        quitConfig.updateMode = AnimatorUpdateMode.UnscaledTime;
        quitControls.updateMode = AnimatorUpdateMode.UnscaledTime;
        controlAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        config1.updateMode = AnimatorUpdateMode.UnscaledTime;
        config2.updateMode = AnimatorUpdateMode.UnscaledTime;
        config3.updateMode = AnimatorUpdateMode.UnscaledTime;
        EventSystem.current.SetSelectedGameObject(continuing.gameObject);
        continuing.onClick.AddListener(Resume);
        config.onClick.AddListener(Configurations);
        controls.onClick.AddListener(Controls);
        quit.onClick.AddListener(QuitToMenu);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(continuing.gameObject);
        }
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
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        EventSystem.current.SetSelectedGameObject(continuing.gameObject);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void Configurations()
    {
        configMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(masterSlider.gameObject);
    }
    public void Controls()
    {
        controlsMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
}
