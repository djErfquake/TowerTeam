using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject leftWall, rightWall, upWall, downWall;
    private GameObject wallSwitch;
    public GameObject decor;


    public GameObject[] GetWalls()
    {
        GameObject[] walls = new GameObject[4];
        walls[0] = leftWall;
        walls[1] = rightWall;
        walls[2] = upWall;
        walls[3] = downWall;
        return walls;
    }

    public void SetDoorway(GameObject wall, bool isDoor)
    {
        wall.SetActive(!isDoor);
    }

    public void SetDoorway(GameObject wall, char doorValue)
    {
        if (doorValue == TileManager.WALL_HORIZONTAL || doorValue == TileManager.WALL_VERTICAL)
        {
            wall.SetActive(true);
        }
        else if (doorValue == TileManager.WALL_SWITCH)
        {
            wall.SetActive(false);
            wallSwitch = Instantiate(TileManager.instance.wallSwitchPrefab);
            wallSwitch.transform.parent = gameObject.transform;
            if (wall == leftWall)
            {
                wallSwitch.transform.localPosition = new Vector2(-3.84f, 0);
            }
            else if (wall == rightWall)
            {
                wallSwitch.transform.localPosition = new Vector2(3.84f, 0);
            }
            else if (wall == upWall)
            {
                wallSwitch.transform.localPosition = new Vector2(0, 3.84f);
                wallSwitch.transform.eulerAngles = new Vector3(0, 0, 90);
            }
            else if (wall == downWall)
            {
                wallSwitch.transform.localPosition = new Vector2(0, -3.84f);
                wallSwitch.transform.eulerAngles = new Vector3(0, 0, 90);
            }
        }
        else
        {
            wall.SetActive(false);
        }
    }


    public void RemoveWallSwitch()
    {
        if (wallSwitch != null)
        {
            Destroy(wallSwitch);
        }
    }


}
