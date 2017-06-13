using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour
{
    public float moveSpeed = 10f, rotateSpeed = 5f;
    //public GameObject landPrefab;
    //public Transform landParent;



    





    public void OnTriggerExit2D(Collider2D collision)
    {
        Vector2 characterPosition = transform.position;
        Vector2 tilePosition = collision.transform.position;
        Vector2 diff = characterPosition - tilePosition;

        
        Vector2 newPosition = Vector2.zero;

        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y)) // went off left or right side
        {
            if (diff.x < 0) // left
            {
                newPosition = new Vector2(tilePosition.x - 1, tilePosition.y);
            }
            else // right
            {
                newPosition = new Vector2(tilePosition.x + 1, tilePosition.y);
            }
        }
        else // went off top or bottom side
        {
            if (diff.y < 0) // bottom
            {
                newPosition = new Vector2(tilePosition.x, tilePosition.y - 1);
            }
            else // top
            {
                newPosition = new Vector2(tilePosition.x, tilePosition.y + 1);
            }
        }

        MapManager.instance.AddRoomTile(newPosition);
    }






    private void Update()
    {
        if (Input.GetKey("up"))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey("down"))
        {
            transform.Translate(Vector3.right * -moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey("left"))
        {
            transform.Rotate(rotateSpeed * Vector3.forward);
        }
        else if (Input.GetKey("right"))
        {
            transform.Rotate(-rotateSpeed * Vector3.forward);
        }
    }

}
