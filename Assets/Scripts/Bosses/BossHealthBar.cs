using UnityEngine;
using UnityEngine.UI;
public class BossHealthBar : MonoBehaviour
{
    public BossCode boss;
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
        float life=boss.bossLife;
        healthImage.fillAmount = (life/100);
    }
}
