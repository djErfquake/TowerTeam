using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject leftWall, rightWall, upWall, downWall;
    public GameObject wallSwitch;
    public GameObject decor;
    public Vector2 roomPosition = Vector2.zero;

    public enum WallSide { up, down, left, right };
    private const float WALL_SWITCH_POSITION = 3.84f;



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

    public void SetWallSwitch(WallSide side)
    {
        wallSwitch = Instantiate(TileManager.instance.wallSwitchPrefab);
        wallSwitch.transform.SetParent(transform);

        switch (side)
        {
            case WallSide.up:
                wallSwitch.transform.localPosition = new Vector2(0, WALL_SWITCH_POSITION);
                wallSwitch.transform.eulerAngles = new Vector3(0, 0, 90);
                SetDoorway(upWall, true);
                break;
            case WallSide.down:
                wallSwitch.transform.localPosition = new Vector2(0, -WALL_SWITCH_POSITION);
                wallSwitch.transform.eulerAngles = new Vector3(0, 0, 90);
                SetDoorway(downWall, true);
                break;
            case WallSide.left:
                wallSwitch.transform.localPosition = new Vector2(-WALL_SWITCH_POSITION, 0);
                SetDoorway(leftWall, true);
                break;
            case WallSide.right:
                wallSwitch.transform.localPosition = new Vector2(WALL_SWITCH_POSITION, 0);
                SetDoorway(rightWall, true);
                break;
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
