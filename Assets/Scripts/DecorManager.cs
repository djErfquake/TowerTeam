using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorManager : MonoBehaviour
{
    [Header("Outdoor")]
    public List<GameObject> outdoorDecor = new List<GameObject>();

    [Header("Indoor")]
    public List<GameObject> indoorDecor = new List<GameObject>();

    private float[] roomRotations = new float[8] { 0, 45, 90, 135, 180, 225, 270, 315 };




    public void DecorateOutdoor(Transform roomTransform)
    {
        GameObject decorations = Instantiate(outdoorDecor[Random.Range(0, outdoorDecor.Count)]);
        decorations.transform.SetParent(roomTransform);
        decorations.transform.localPosition = Vector2.zero;
        decorations.transform.localScale = Vector2.one;
        //decorations.transform.Rotate(Vector3.forward, roomRotations[Random.Range(0, roomRotations.Length)]); // has to rotate items in room too
    }



    public void DecorateIndoor(Transform roomTransform)
    {
        GameObject decorations = Instantiate(indoorDecor[Random.Range(0, indoorDecor.Count)]);
        decorations.transform.SetParent(roomTransform);
        decorations.transform.localPosition = Vector2.zero;
        decorations.transform.localScale = Vector2.one;
        //decorations.transform.Rotate(Vector3.forward, roomRotations[Random.Range(0, roomRotations.Length)]);
    }





    // singleton
    public static DecorManager instance;
    private void Awake()
    {
        if (instance && instance != this) { Destroy(gameObject); return; }
        instance = this;
    }


}
