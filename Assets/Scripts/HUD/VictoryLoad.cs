using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryLoad : MonoBehaviour
{
    private void Start()
    {
        if (GlobalVariables.score > GlobalVariables.highScore)
    {
    GlobalVariables.highScore = GlobalVariables.score;
    PlayerPrefs.SetInt("HighScore", GlobalVariables.highScore);
    PlayerPrefs.Save();
    }
        GlobalVariables.currentScore = GlobalVariables.score;
        GlobalVariables.waveCounter = 0;
        GlobalVariables.bossCounter = 0;
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Z) && GlobalVariables.phaseCounter == 2)
        {
            GlobalVariables.score = GlobalVariables.currentScore;
            SceneManager.LoadScene("Phase2");
        }
        else if (Input.GetKey(KeyCode.Z) && GlobalVariables.phaseCounter == 6)
        {
            GlobalVariables.score = GlobalVariables.currentScore;
            SceneManager.LoadScene("Phase3");
        }
        else if (Input.GetKey(KeyCode.Z) && GlobalVariables.phaseCounter == 8)
        {
            GlobalVariables.score = GlobalVariables.currentScore;
            SceneManager.LoadScene("Phase4");
        }else if (Input.GetKey(KeyCode.Z) && GlobalVariables.phaseCounter == 9)
        {
            GlobalVariables.score = GlobalVariables.currentScore;
            SceneManager.LoadScene("Credits");
        }
    }
}

