using UnityEngine;

[ExecuteInEditMode]
public class GridSnap : MonoBehaviour
{
    void Update()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        position.z = Mathf.Round(position.z);
        transform.position = position;
    }
}
