using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemCollection.instance.AddItem(GetComponent<SpriteRenderer>().sprite);
        Destroy(gameObject);
    }


}
