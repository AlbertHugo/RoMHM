using UnityEngine;
using UnityEngine.UI;
public class Boss2Health : MonoBehaviour
{
    public Boss2Code boss;
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
        healthImage.fillAmount = (life / 200);
    }
}
