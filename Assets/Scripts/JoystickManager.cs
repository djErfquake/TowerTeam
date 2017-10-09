using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickManager : MonoBehaviour
{
    public int numOfJoysticks = -1; // ensures updates automatically

    [Header("Explorers")]
    public GameObject[] explorers = new GameObject[4];

    [Header("Game Cameras")]
    public GameObject[] gameCameras = new GameObject[4];




    private void Update()
    {
        // get count of connected joysticks
        int newNumberOfJoysticks = Input.GetJoystickNames().Length;

        // keyboard if there are no other joysticks
        if (newNumberOfJoysticks == 0)
        {
            newNumberOfJoysticks = 1;
        }
        
        // update game with how many players are connected
        if (newNumberOfJoysticks != numOfJoysticks)
        {
            numOfJoysticks = newNumberOfJoysticks;
            SetupForMode(GameManager.instance.mode);
        }
    }



    public void SetupForMode(GameManager.Mode newMode)
    {
        if (GameManager.instance.mode == GameManager.Mode.Game || GameManager.instance.mode == GameManager.Mode.Pause)
        {
            for (int i = 0; i < explorers.Length; i++)
            {
                explorers[i].SetActive(false);
                gameCameras[i].SetActive(false);
            }

            gameCameras[numOfJoysticks - 1].SetActive(true);
            for (int i = 0; i < numOfJoysticks; i++)
            {
                explorers[i].SetActive(true);
            }
        }
    }




    public void KeyboardPlaying()
    {
        explorers[0].SetActive(true);
        gameCameras[0].SetActive(true);
    }






    // singleton
    public static JoystickManager instance;
    private void Awake()
    {
        if (instance && instance != this) { Destroy(gameObject); return; }
        instance = this;
    }


}
