using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    public Text diaglogue;


    private List<string> allText;

    private Coroutine typingCoroutine;
    private int currentTextIndex = 0;
    private int currentLetterIndex = 0;

    [HideInInspector]
    public bool shown = false;



    public void SetText(List<string> dialogueText)
    {
        allText = dialogueText;
    }



    // for testing purposes only
    private void Update()
    {
        if (shown && Input.GetKeyDown(KeyCode.Z))
        {
            if (currentLetterIndex == allText[currentTextIndex].Length)
            {
                Next();
            }
            else
            {
                diaglogue.text = allText[currentTextIndex];
                currentLetterIndex = allText[currentTextIndex].Length;
            }
        }
    }



    public void Show()
    {
        currentTextIndex = 0;
        currentLetterIndex = 0;
        diaglogue.text = "";
        shown = true;

        GetComponent<RectTransform>().DOAnchorPosY(-540f, 0.5f).SetEase(Ease.OutBack).OnComplete(() => { typingCoroutine = StartCoroutine(TypeWords()); });
    }

    
    private IEnumerator TypeWords()
    {
        while (currentLetterIndex != allText[currentTextIndex].Length)
        {
            diaglogue.text += allText[currentTextIndex][currentLetterIndex];
            currentLetterIndex++;

            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Next()
    {
        currentTextIndex++;
        if (currentTextIndex >= allText.Count)
        {
            Hide();
        }
        else
        {
            currentLetterIndex = 0;
            diaglogue.text = "";
            typingCoroutine = StartCoroutine(TypeWords());
        }
    }

    public void Hide()
    {
        shown = false;
        GetComponent<RectTransform>().DOAnchorPosY(-845f, 0.5f).SetEase(Ease.InBack);
    }




    #region singleton
    // singleton
    public static DialogueManager instance;
    private void Awake()
    {
        if (instance && instance != this) { Destroy(gameObject); return; }
        instance = this;

        Hide();
    }
    #endregion
}
