using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{

    public List<Sprite> availableCharacters = new List<Sprite>();
    public GameObject characterChoicePrefab;
    public Transform characterChoiceContainer;

    private List<GameObject> characterChoices = new List<GameObject>();
    private int currentIndex = 0, lastIndex = 0;
    private bool moving = false;
    private Sprite currentCostume;


    private void Start()
    {
        foreach (Sprite characterImage in availableCharacters)
        {
            GameObject characterChoice = Instantiate(characterChoicePrefab);
            characterChoice.transform.SetParent(characterChoiceContainer);
            characterChoice.transform.localScale = Vector3.one;
            characterChoice.transform.localPosition = new Vector2(350f, -5f);
            characterChoice.name = characterImage.name;
            characterChoice.GetComponent<Image>().sprite = characterImage;

            characterChoices.Add(characterChoice);
        }

        characterChoices[0].transform.localPosition = new Vector2(0, -5f);
    }


    public void NextCharacter()
    {
        if (!moving)
        {
            lastIndex = currentIndex;
            currentIndex++;
            if (currentIndex >= characterChoices.Count) { currentIndex = 0; }

            characterChoices[currentIndex].transform.localPosition = new Vector2(350f, -5f);
            characterChoices[currentIndex].transform.DOLocalMoveX(0, 1f).SetEase(Ease.InOutBack);
            characterChoices[lastIndex].transform.DOLocalMoveX(-350f, 1f).SetEase(Ease.InOutBack).OnComplete(() => { moving = false; });

            moving = true;
        }
    }


    public void PreviousCharacter()
    {
        if (!moving)
        {
            lastIndex = currentIndex;
            currentIndex--;
            if (currentIndex < 0) { currentIndex = characterChoices.Count - 1; }

            characterChoices[currentIndex].transform.localPosition = new Vector2(-350f, -5f);
            characterChoices[currentIndex].transform.DOLocalMoveX(0, 1f).SetEase(Ease.InOutBack);
            characterChoices[lastIndex].transform.DOLocalMoveX(350f, 1f).SetEase(Ease.InOutBack).OnComplete(() => { moving = false; });

            moving = true;
        }
    }


    public Sprite GetSelectedCharacter()
    {
        return availableCharacters[currentIndex];
    }

    public void RemoveSelectedCharacter()
    {
        availableCharacters.RemoveAt(currentIndex);

        GameObject selectedCharacterChoice = characterChoices[currentIndex];
        characterChoices.RemoveAt(currentIndex);
        Destroy(selectedCharacterChoice);

        lastIndex = currentIndex - 1;
        if (lastIndex < 0) { lastIndex = characterChoices.Count - 1; }
        currentIndex++;
        if (currentIndex >= characterChoices.Count) { currentIndex = 0; }

        characterChoices[currentIndex].transform.localPosition = new Vector2(350f, -5f);
        characterChoices[currentIndex].transform.DOLocalMoveX(0, 1f).SetEase(Ease.InOutBack);
        characterChoices[lastIndex].transform.DOLocalMoveX(-350f, 1f).SetEase(Ease.InOutBack).OnComplete(() => { moving = false; });

        moving = true;
    }
}
