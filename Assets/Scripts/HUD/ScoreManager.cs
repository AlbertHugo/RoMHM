using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 
    void Update()
    {
        scoreText.text = "SCORE: " + GlobalVariables.score.ToString();
    }
}