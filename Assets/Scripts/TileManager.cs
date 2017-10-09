using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static int DOORWAY_LEFT = 0;
    public static int DOORWAY_RIGHT = 0;
    public static int DOORWAY_UP = 0;
    public static int DOORWAY_DOWN = 0;


    public static char ROOM_EMPTY = ' ';
    public static char ROOM_NONE = '1';

    public static char ROOM_START = 'S';

    public static char ROOM_BLOCKER = 'B';
    public static char ROOM_PART = 'm';
    public static char ROOM_PART_ASSEMBLY = 'M';
    public static char ROOM_SWITCH = 's';
    public static char ROOM_POWERUP_PART = 'p';
    public static char ROOM_POWERUP_ASSEMBLY = 'P';
    public static char ROOM_WALL_SWITCH = 'd';


    public static char WALL_NONE = ' ';
    public static char WALL_VERTICAL = '|';
    public static char WALL_HORIZONTAL = '-';
    public static char WALL_SWITCH = 'D';




    [Header("Room Info")]
    public GameObject roomPrefab;
    public Transform roomParent;



    // styling
    [Header("Styling")]
    public List<Sprite> floors = new List<Sprite>();


    // prefabs
    [Header("Prefabs")]
    public GameObject itemPrefab;

    [Header("Wall Switch")]
    public GameObject wallSwitchPrefab, wallSwitchButtonPrefab;

    [Header("Multi-Part Assembly")]
    public Sprite partA, partB, partC;
    public GameObject partAssemblyPrefab;

    [Header("Blocker / Boulder")]
    public GameObject blockerPrefab;




    public GameObject CreateTile(Vector2 pos)
    {
        GameObject newTile = Instantiate(roomPrefab);
        newTile.transform.parent = roomParent;
        newTile.transform.position = pos;

        return newTile;
    }


    public GameObject RandomFloor(SpriteRenderer spriteRenderer, Transform roomTransform)
    {
        Sprite randomFloor = floors[Random.Range(0, floors.Count)];
        spriteRenderer.sprite = randomFloor;

        GameObject decorObject;
        if (randomFloor.name == "Grass")
        {
            decorObject = DecorManager.instance.DecorateOutdoor(roomTransform);
        }
        else
        {
            decorObject = DecorManager.instance.DecorateIndoor(roomTransform);
        }

        return decorObject;
    }






    // singleton
    public static TileManager instance;
    private void Awake()
    {
        if (instance && instance != this) { Destroy(gameObject); return; }
        instance = this;
    }

}
