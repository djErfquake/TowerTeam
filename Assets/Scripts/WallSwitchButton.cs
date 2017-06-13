
using UnityEngine;

public class WallSwitchButton : MonoBehaviour
{
    public Sprite activatedSprite;

    private bool activated = false;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated)
        {
            MapManager.instance.WallSwitchActivated();
            gameObject.GetComponent<SpriteRenderer>().sprite = activatedSprite;
            activated = true;
        }
        
    }

}
