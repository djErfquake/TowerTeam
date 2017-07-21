using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    public GameObject itemPrefab;
    public Explorer explorer;

    public List<GameObject> items = new List<GameObject>();


    public void AddItem(Sprite sprite)
    {
        GameObject newItem = Instantiate(itemPrefab);
        newItem.transform.SetParent(gameObject.transform);
        newItem.name = sprite.name;
        newItem.GetComponent<Image>().sprite = sprite;
        newItem.GetComponent<Button>().onClick.AddListener(() => ItemClicked(sprite));

        items.Add(newItem);
    }


    public void RemoveItem(Sprite sprite)
    {
        GameObject destroyItem = null;

        foreach (GameObject item in items)
        {
            if (item.GetComponent<Image>().sprite == sprite)
            {
                destroyItem = item;
            }
        }

        if (destroyItem != null)
        {
            items.Remove(destroyItem);
            Destroy(destroyItem);
        }
    }

    public void ItemClicked(Sprite item)
    {
        explorer.ItemClicked(item);
    }




    // singleton
    public static ItemCollection instance;
    private void Awake()
    {
        if (instance && instance != this) { Destroy(gameObject); return; }
        instance = this;
    }


}
