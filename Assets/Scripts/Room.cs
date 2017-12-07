using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject leftWall, rightWall, upWall, downWall;
    public GameObject wallSwitch;
    public GameObject decor;
    public Vector2 roomPosition = Vector2.zero;


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


    public void RemoveWallSwitch()
    {
        if (wallSwitch != null)
        {
            Destroy(wallSwitch);
        }
    }


}
