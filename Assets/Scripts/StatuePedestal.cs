using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatuePedestal : MonoBehaviour
{
    public List<Sprite> spriteProgression = new List<Sprite>();
    public List<Sprite> itemProgression = new List<Sprite>();

    private int itemIndex = 0;

    [HideInInspector]
    public bool completed = false;

    public Action completeAction;




    public bool AddItem(Sprite item)
    {
        if (itemProgression[itemIndex] == item)
        {
            itemIndex++;
            GetComponent<SpriteRenderer>().sprite = spriteProgression[itemIndex];

            if (itemIndex == itemProgression.Count)
            {
                completed = true;
                completeAction();
            }

            return true;
        }

        return false;
    }

    


}
