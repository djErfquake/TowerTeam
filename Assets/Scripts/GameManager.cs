using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<Explorer> explorers = new List<Explorer>();
    




    // singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance && instance != this) { Destroy(gameObject); return; }
        instance = this;
    }


}
