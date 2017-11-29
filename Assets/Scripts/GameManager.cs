using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum Mode
    {
        Start,
        CharacterSelection,
        Game,
        Pause
    };
    public Mode mode = Mode.Start;


    [Header("Explorers")]
    public List<Explorer> explorerControllers = new List<Explorer>();
    public List<Explorer> explorers = new List<Explorer>();



    [Header("Main Menu")]
    public GameObject menuBackground;
    public RectTransform mainMenu;
    public Button startGameButton;

    [Header("Character Selection Menu")]
    public RectTransform characterMenu;
    public Button rightButton;
    public Text characterSelectionInstructionText;
    private int readyCharacters = 0;

    [Header("Game")]
    public GameObject gameScreen;




    



    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(startGameButton.gameObject);
        mode = Mode.Start;
    }


    public void GoToCharacterSelectionScreen()
    {
        readyCharacters = 0;

        characterMenu.localPosition = new Vector2(0, -1080f);
        characterMenu.gameObject.SetActive(true);

        mainMenu.DOLocalMoveY(1080f, 1.5f).SetEase(Ease.InOutBack);
        characterMenu.DOLocalMoveY(0, 1.5f).SetEase(Ease.InOutBack).OnComplete(()=> { OnCharacterSelectionScreen(); });

        mode = Mode.CharacterSelection;
        JoystickManager.instance.SetupForMode(mode);
    }

    private void OnCharacterSelectionScreen()
    {
        mainMenu.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(rightButton.gameObject);
    }



    public void CharacterSelected(CharacterSelection characterSelection)
    {
        readyCharacters++;
        Explorer newExplorer = explorerControllers[readyCharacters - 1];
        explorers.Add(newExplorer);
        newExplorer.SetCostume(characterSelection.GetSelectedCharacter());
        characterSelection.RemoveSelectedCharacter();

        Debug.Log(readyCharacters + "/" + JoystickManager.instance.numOfJoysticks + " characters ready");
        if (readyCharacters >= JoystickManager.instance.numOfJoysticks)
        {
            // go!
            MapManager.instance.LoadTrainingLevel(0);

            Vector3 startingPosition = MapManager.instance.startingPosition;
            startingPosition.x -= 0.15f * (explorers.Count - 1);
            foreach (Explorer explorer in explorers)
            {
                explorer.gameObject.SetActive(true);
                explorer.transform.position = explorer.transform.position + startingPosition;
                startingPosition.x += 0.2f;
            }

            gameScreen.SetActive(true);
            menuBackground.SetActive(false);
            characterMenu.gameObject.SetActive(false);

            mode = Mode.Game;
            JoystickManager.instance.SetupForMode(mode);

            DialogueManager.instance.Show();
        }
        else
        {
            characterSelectionInstructionText.text = "Costume for Player " + (readyCharacters + 1).ToString();
        }

        
    }




    public void ExitGame()
    {
        Application.Quit();
    }






















    // singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance && instance != this) { Destroy(gameObject); return; }
        instance = this;
    }


}
