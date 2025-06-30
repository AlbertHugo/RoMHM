using UnityEngine;
using UnityEngine.UI;

public class Boss4Health : MonoBehaviour
{
    public Boss4Code boss;
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
        healthImage.fillAmount = life / 1000;
    }
}
