using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryVerify : MonoBehaviour
{
    void Start()
    {
        GlobalVariables.waveCounter = 0;
        GlobalVariables.spawnCount = 0;
        GlobalVariables.bossCounter = 0;
        GlobalVariables.revengeCount = 0;
        if (SceneManager.GetActiveScene().name == "Phase1")
        {
            GlobalVariables.score=0;
            GlobalVariables.currentScore = 0;
            GlobalVariables.isSelecting = false;
            GlobalVariables.phaseCounter = 1;
        }
        else if (SceneManager.GetActiveScene().name == "Boss1")
        {
            GlobalVariables.phaseCounter = 2;
        }
        else if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            GlobalVariables.phaseCounter = 4;
        }
        else if (SceneManager.GetActiveScene().name == "Phase2")
        {
            GlobalVariables.phaseCounter = 5;
        }else if(SceneManager.GetActiveScene().name == "Boss2")
        {
            GlobalVariables.phaseCounter = 6;
        }else if(SceneManager.GetActiveScene().name == "Phase3")
        {
            GlobalVariables.phaseCounter = 7;
        }else if(SceneManager.GetActiveScene().name == "Boss3")
        {
            GlobalVariables.phaseCounter = 8;
        }
        else if (SceneManager.GetActiveScene().name == "Phase4")
        {
            GlobalVariables.phaseCounter = 9;
        }
        if (GlobalVariables.isSelecting == true)
        {
            GlobalVariables.score = 0;
            GlobalVariables.currentScore = 0;
        }
    }
}
