using System.Collections.Generic;
using UnityEngine;

public class ExitStairs : MonoBehaviour
{
    public List<SpriteRenderer> stairs = new List<SpriteRenderer>();

    [HideInInspector]
    public bool opened = false;

    private void Start()
    {
        Close();
    }

    // testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (opened)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }

    public void Open()
    {
        Color stairColor = Color.white;
        for (int i = 0; i < stairs.Count; i++)
        {
            stairColor.a -= 50f/255f;
            stairs[i].color = stairColor;
        }
        opened = true;
    }

    public void Close()
    {
        for (int i = 0; i < stairs.Count; i++)
        {
            stairs[i].color = Color.white;
        }
        opened = false;
    }


    #region singleton
    // singleton
    public static ExitStairs instance;
    private void Awake()
    {
        if (instance && instance != this) { Destroy(gameObject); return; }
        instance = this;
    }
    #endregion

}
