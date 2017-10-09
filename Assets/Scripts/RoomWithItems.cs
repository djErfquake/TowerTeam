using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWithItems : MonoBehaviour
{
    public List<Vector3> itemPositions = new List<Vector3>();

    private float[] roomRotations = new float[8] { 0, 45, 90, 135, 180, 225, 270, 315 };
    public List<float> unsupportedRotations = new List<float>();


    public Vector2 GetRandomItemPosition()
    {
        int index = Random.Range(0, itemPositions.Count);
        Vector3 itemPosition = itemPositions[index];
        itemPositions.RemoveAt(index);
        return itemPosition;
    }


    public float GetRandomRotation()
    {
        bool rotationUnsupported = true;
        float randomRotation = 0;
        do
        {
            rotationUnsupported = false;
            randomRotation = roomRotations[Random.Range(0, roomRotations.Length)];

            for (int i = 0; i < unsupportedRotations.Count; i++)
            {
                 if (randomRotation == unsupportedRotations[i])
                {
                    rotationUnsupported = true;
                    break;
                }
            }  

        } while (rotationUnsupported);

        return randomRotation;
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
