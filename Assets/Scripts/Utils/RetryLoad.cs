using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryLoad : MonoBehaviour
{
    private void Start()
    {
        if (GlobalVariables.score > GlobalVariables.highScore)
    {
    GlobalVariables.highScore = GlobalVariables.score;
    PlayerPrefs.SetInt("HighScore", GlobalVariables.highScore);
    PlayerPrefs.Save();
    }
        GlobalVariables.waveCounter = 0;
        GlobalVariables.bossCounter = 0;
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Z) && GlobalVariables.phaseCounter == 1)
        {
            GlobalVariables.score = 0;
            SceneManager.LoadScene("Phase1");
        }
        else if (Input.GetKey(KeyCode.Z) && GlobalVariables.phaseCounter == 2)
        {
            GlobalVariables.score = GlobalVariables.currentScore;
            SceneManager.LoadScene("Boss1");
        }
        else if (Input.GetKey(KeyCode.Z) && GlobalVariables.phaseCounter == 3)
        {
            GlobalVariables.score = 0;
            SceneManager.LoadScene("MainMenu");
        }
        else if (Input.GetKey(KeyCode.Z) && GlobalVariables.phaseCounter == 4)
        {
            GlobalVariables.score = 0;
            SceneManager.LoadScene("Tutorial");
        }
        else if (Input.GetKey(KeyCode.Z) && GlobalVariables.phaseCounter == 5)
        {
            GlobalVariables.score = GlobalVariables.currentScore;
            SceneManager.LoadScene("Phase2");
        }
        else if (Input.GetKey(KeyCode.Z) && GlobalVariables.phaseCounter == 6)
        {
            GlobalVariables.score = GlobalVariables.currentScore;
            SceneManager.LoadScene("Boss2");
        }else if(Input.GetKey(KeyCode.Z) && GlobalVariables.phaseCounter == 7)
        {
            GlobalVariables.score = GlobalVariables.currentScore;
            SceneManager.LoadScene("Phase3");
        }else if(Input.GetKey(KeyCode.Z) && GlobalVariables.phaseCounter == 8)
        {
            GlobalVariables.score = GlobalVariables.currentScore;
            SceneManager.LoadScene("Boss3");
        }
        else if (Input.GetKey(KeyCode.Z) && GlobalVariables.phaseCounter == 9)
        {
            GlobalVariables.score = GlobalVariables.currentScore;
            SceneManager.LoadScene("Phase4");
        }
    }
}
