using NiceJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Vector2 size = new Vector2(5, 3);

    private Dictionary<Vector2, GameObject> roomTiles = new Dictionary<Vector2, GameObject>();
    private GameObject startingRoomTile;

    private TileManager tileManager;

    private void Start()
    {
        tileManager = TileManager.instance;


        JsonNode levelConfig = ConfigHelper.Load(Application.streamingAssetsPath + "/levelConfig.json");
        JsonNode level = levelConfig["levels"][0];


        for (int r = 0; r < size.y; r++)
        {
            string roomRowString = level["rooms"][r * 2];
            char[] roomRow = roomRowString.ToCharArray();

            string previousRow = "-+-+-+-+-";
            string nextRow = "-+-+-+-+-";

            if (r > 0)
            {
                previousRow = level["rooms"][(r * 2) - 1];
            }
            if (r < size.y - 1)
            {
                nextRow = level["rooms"][(r * 2) + 1];
            }

            for (int c = 0; c < size.x; c++)
            {
                string tileName = r.ToString() + c.ToString();
                GameObject roomTile = GameObject.Find(tileName);
                roomTile.GetComponent<SpriteRenderer>().sprite = tileManager.RandomFloor();
                Room room = roomTile.GetComponent<Room>();

                //RandomizeDoorways(roomTile);
                //roomTile.SetActive(false);

                float xPos = c - Mathf.FloorToInt(size.x / 2);
                float yPos = r - Mathf.FloorToInt(size.y / 2);
                roomTiles.Add(new Vector2(xPos, yPos), roomTile);



                // show room
                char roomLetter = roomRow[c * 2];
                if (roomLetter == TileManager.ROOM_NONE)
                {
                    roomTile.SetActive(false);
                }
                else
                {
                    if (roomLetter == TileManager.ROOM_WALL_SWITCH)
                    {
                        GameObject wallSwitch = Instantiate(TileManager.instance.wallSwitchButtonPrefab);//, Vector2.zero, Quaternion.identity, roomTile.transform);
                        wallSwitch.transform.parent = roomTile.transform;
                        wallSwitch.transform.localPosition = Vector2.zero;
                    }
                }

                char previousRoomLetter = TileManager.WALL_VERTICAL;
                char nextRoomLetter = TileManager.WALL_VERTICAL;
                if (c > 0)
                {
                    previousRoomLetter = roomRow[(c * 2) - 1];
                }
                if (c < size.x - 1)
                {
                    nextRoomLetter = roomRow[(c * 2) + 1];
                }

                // set walls
                room.SetDoorway(room.leftWall, previousRoomLetter);
                room.SetDoorway(room.rightWall, nextRoomLetter);
                room.SetDoorway(room.upWall, previousRow.ToCharArray()[c * 2]);
                room.SetDoorway(room.downWall, nextRow.ToCharArray()[c * 2]);



            }
        }




    }




    public void RandomizeDoorways(GameObject roomTile)
    {
        GameObject[] walls = roomTile.GetComponent<Room>().GetWalls();
        for (int i = 0; i < walls.Length; i++)
        {
            int isWall = Random.Range(0, 2);
            walls[i].SetActive(isWall == 1);
        }
        

        // check for pathway to startingroom
    }






    public void AddRoomTile(Vector2 tilePosition)
    {
        if (!RoomTileExists(tilePosition))
        {
            Debug.Log("adding room at " + tilePosition);
            GameObject roomTile = tileManager.CreateTile(tilePosition);
            roomTiles.Add(roomTile.transform.position, roomTile);
        }
    }

    public GameObject GetRoomTile(Vector2 tilePosition)
    {
        if (roomTiles.ContainsKey(tilePosition))
        {
            return roomTiles[tilePosition];
        }

        return null;
    }

    public bool RoomTileExists(Vector2 tilePosition)
    {
        return roomTiles.ContainsKey(tilePosition);
    }




    // maze logic
    public void WallSwitchActivated()
    {
        foreach (KeyValuePair<Vector2, GameObject> room in roomTiles)
        {
            room.Value.GetComponent<Room>().RemoveWallSwitch();
        }
    }








    // singleton
    public static MapManager instance;
    private void Awake()
    {
        if (instance && instance != this) { Destroy(gameObject); return; }
        instance = this;
    }
}
