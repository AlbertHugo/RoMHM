using UnityEngine;
using UnityEngine.UI;

public class Boss3Health : MonoBehaviour
{
    public Boss3Code boss;
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
        healthImage.fillAmount = (life / 400);
    }
}
