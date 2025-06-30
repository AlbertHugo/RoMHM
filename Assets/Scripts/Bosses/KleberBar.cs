using UnityEngine;
using UnityEngine.UI;

public class KleberBar : MonoBehaviour
{
    public Boss1Code boss;
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
        float life = boss.bossLife;
        healthImage.fillAmount = (life / 100);
    }
}

