using NiceJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // components
    private TileManager tileManager;
    private DialogueManager dialogue;

    // level
    private JsonNode levelConfig;
    private Texture2D map;
    public Transform mapContainer;
    public GameObject roomPrefab;
    private List<Room> rooms = new List<Room>();


    // variables
    [HideInInspector]
    public Vector2 startingPosition;


    private void Start()
    {
        tileManager = TileManager.instance;
        dialogue = DialogueManager.instance;
        levelConfig = ConfigHelper.Load(Application.streamingAssetsPath + "/levelConfig.json");
    }


    public void LoadTrainingLevel(int index)
    {
        LoadLevel(levelConfig["training-levels"][index]);
    }

    public void LoadNormalLevel(int index)
    {
        LoadLevel(levelConfig["levels"][index]);
    }


    private void LoadLevel(JsonNode level)
    {
        // delete any old rooms to start from scratch
        while (rooms.Count > 0)
        {
            Room r = rooms[0];
            rooms.RemoveAt(0);
            Destroy(r);
        }

        // load new dialogue
        List<string> dialogueText = new List<string>();
        JsonArray dialogueConfig = level["dialogue"] as JsonArray;
        for (int i = 0; i < dialogueConfig.Count; i++)
        {
            dialogueText.Add(dialogueConfig[i]);
        }
        dialogue.SetText(dialogueText);


        // load rooms and add decor and items
        LoadMap(Application.streamingAssetsPath + "/Maps/" + level["map"]);
    }




    private void LoadMap(string imagePath)
    {
        Debug.Log("loading map from " + imagePath);

        // load file
        map = new Texture2D(2, 2);
        byte[] fileData = File.ReadAllBytes(imagePath);
        map.LoadImage(fileData);

        // create rooms
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                if (x % 2 == 1 && y % 2 == 1)
                {
                    GenerateRoom(x, y);
                }
                else if (x % 2 == 0 || y % 2 == 0)
                {
                    //GenerateDoorway(x, y);
                }
            }
        }
    }

    private void GenerateRoom(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor != Color.black)
        {
            int roomPositionX = Mathf.RoundToInt(x / 2);
            int roomPositionY = Mathf.RoundToInt(y / 2);

            GameObject roomGo = Instantiate(roomPrefab);
            roomGo.name = "Room " + roomPositionX + "," + roomPositionY;
            roomGo.transform.SetParent(mapContainer);
            roomGo.transform.localPosition = new Vector2(roomPositionX, roomPositionY);

            // do decor

            // do items

        }
    }







    // maze logic
    public void WallSwitchActivated()
    {
        //foreach (KeyValuePair<Vector2, GameObject> room in roomTiles)
        //{
        //    room.Value.GetComponent<Room>().RemoveWallSwitch();
        //}
    }





    // singleton
    public static MapManager instance;
    private void Awake()
    {
        if (instance && instance != this) { Destroy(gameObject); return; }
        instance = this;
    }
}
