using UnityEngine;
using UnityEngine.UI;

public class BulletType : MonoBehaviour
{
    public Image bulletPortrait;
    public Player2Code player;
    private Sprite[] images;
    void Start()
    {
        images = new Sprite[]
        {
            Resources.Load<Sprite>("HUD/BulletframeDestructive"),
            Resources.Load<Sprite>("HUD/BulletframeFocused"),
            Resources.Load<Sprite>("HUD/BulletframeRegular")
        };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.isDestructive&&player.focus==false)
        {
            bulletPortrait.sprite = images[0];
        }else if(player.isDestructive==false && player.focus == false)
        {
            bulletPortrait.sprite = images[2];
        }else if(player.focus==true) 
        {
            bulletPortrait.sprite=images[1];
        }
    }
}
