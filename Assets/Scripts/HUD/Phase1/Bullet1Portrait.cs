using UnityEngine;
using UnityEngine.UI;

public class Bullet1Portrait : MonoBehaviour
{
    public Image bulletPortrait;
    public PlayerCode player;
    private Sprite[] images;
    void Start()
    {
        images = new Sprite[]
        {
            Resources.Load<Sprite>("HUD/BulletframeFocused"),
            Resources.Load<Sprite>("HUD/BulletframeRegular")
        };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.focus == false)
        {
            bulletPortrait.sprite = images[1];
        }
        else if (player.focus == true)
        {
            bulletPortrait.sprite = images[0];
        }
    }
}
