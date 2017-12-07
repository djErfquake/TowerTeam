using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static int DOORWAY_LEFT = 0;
    public static int DOORWAY_RIGHT = 0;
    public static int DOORWAY_UP = 0;
    public static int DOORWAY_DOWN = 0;


    public Color ROOM_START = Color.green;
    public Color ROOM_END = Color.red;

    public Color ROOM_BLOCKER = Color.cyan;
    public Color ROOM_PART;
    public Color ROOM_PART_ASSEMBLY;
    public Color ROOM_SWITCH;
    public Color ROOM_POWERUP_PART;
    public Color ROOM_POWERUP_ASSEMBLY;
    public Color ROOM_WALL_SWITCH;
    public Color WALL_SWITCH;



    // styling
    [Header("Styling")]
    public List<Sprite> floors = new List<Sprite>();


    // prefabs
    [Header("End Stairs")]
    public GameObject endPrefab;

    [Header("Prefabs")]
    public GameObject itemPrefab;

    [Header("Wall Switch")]
    public GameObject wallSwitchPrefab, wallSwitchButtonPrefab;

    [Header("Multi-Part Assembly")]
    public Sprite partA, partB, partC;
    public GameObject partAssemblyPrefab;

    [Header("Blocker / Boulder")]
    public GameObject blockerPrefab;


    public GameObject GenerateRandomDecor(SpriteRenderer spriteRenderer, Transform roomTransform)
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
