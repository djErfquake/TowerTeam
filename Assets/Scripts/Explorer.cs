using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour
{
    public float moveSpeed = 10f, rotateSpeed = 5f;
    private float initialMoveSpeed = 0.5f;

    private  Vector3 itemHoldOffset = Vector3.up * 0.05f; 
    private GameObject itemInRange;
    private List<GameObject> nearbyPedestals = new List<GameObject>();


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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            initialMoveSpeed = moveSpeed;
            moveSpeed = 1.5f;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            moveSpeed = initialMoveSpeed;
        }

        if (itemInRange != null && Input.GetKey(KeyCode.Z))
        {
            itemInRange.transform.position = transform.position + itemHoldOffset;
        }
        else if (itemInRange != null && Input.GetKeyUp(KeyCode.Z))
        {
            foreach (GameObject nextToGo in nearbyPedestals)
            {
                StatuePedestal pedestal = nextToGo.GetComponent<StatuePedestal>();
                if (pedestal != null && pedestal.AddItem(itemInRange.GetComponent<SpriteRenderer>().sprite))
                {
                    Destroy(itemInRange);
                }
            }
        }
    }




    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;

        if (collider.CompareTag("Item"))
        {
            itemInRange = collider;
        }
        else if (collider.CompareTag("Pedestal"))
        {
            nearbyPedestals.Add(collider);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;

        if (collider.CompareTag("Item"))
        {
            itemInRange = null;
        }
        else if (collider.CompareTag("Pedestal") && nearbyPedestals.Contains(collider))
        {
            nearbyPedestals.Remove(collider);
        }
    }







    //public GameObject landPrefab;
    //public Transform landParent;

    /*
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
    */

}
