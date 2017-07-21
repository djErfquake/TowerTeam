using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWithItems : MonoBehaviour
{
    public List<Vector3> itemPositions = new List<Vector3>();


    public Vector2 GetRandomItemPosition()
    {
        int index = Random.Range(0, itemPositions.Count);
        Vector3 itemPosition = itemPositions[index];
        itemPositions.RemoveAt(index);
        return itemPosition;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        foreach (Vector3 itemPosition in itemPositions)
        {
            Gizmos.DrawSphere(transform.position + itemPosition, 0.05f);
        }
        
    }


}
