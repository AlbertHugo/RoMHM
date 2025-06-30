using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public PlayerCode player;
    private Image healthImage;

    void Start()
    {
        healthImage = GetComponent<Image>();
    }

    void FixedUpdate()
    {
            UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float life = player.life;

        if (life >= 3)
            healthImage.fillAmount = 1f;
        else if (life == 2)
            healthImage.fillAmount = 0.7f;
        else if (life == 1)
            healthImage.fillAmount = 0.4f;
        else if (life <= 0||player==null)
            healthImage.fillAmount = 0f;
    }

}