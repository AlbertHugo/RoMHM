using UnityEngine;
using UnityEngine.UI;

public class Bullet3Portrait : MonoBehaviour
{
    public Image bulletPortrait;
    public Player3Code player;
    private Sprite[] images;
    void Start()
    {
        images = new Sprite[]
        {
            Resources.Load<Sprite>("HUD/BulletframeDestructive"),
            Resources.Load<Sprite>("HUD/BulletframeFocused"),
            Resources.Load<Sprite>("HUD/BulletframeRegular"),
            Resources.Load<Sprite>("HUD/Bulletframe Charged")
        };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.isDestructive && player.focus == false)
        {
            bulletPortrait.sprite = images[0];
        }
        else if (player.isDestructive == false && player.focus == false)
        {
            bulletPortrait.sprite = images[2];
        }
        else if (player.focus == true && player.isDestructive)
        {
            bulletPortrait.sprite = images[3];
        }else if (player.focus)
        {
            bulletPortrait.sprite = images[1];
        }
    }
}