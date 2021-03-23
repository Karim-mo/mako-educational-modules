using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    public TextMeshProUGUI btnText;
    [HideInInspector]
    public bool Correct;
    private RectTransform myRect;
    public RectTransform textRect;


    private void Start()
        => myRect = GetComponent<RectTransform>();


    public void initButton(string text, bool correct)
    {
        btnText.text = text;
        Correct = correct;
        StartCoroutine(SetButtonWidth());
    }
    public IEnumerator SetButtonWidth()
    {
        yield return new WaitForEndOfFrame();
        myRect.sizeDelta = new Vector2(textRect.rect.width + 40, myRect.sizeDelta.y + 10);
    }
    public void initButton(string text, bool correct, Vector2 size)
    {
        btnText.text = text;
        Correct = correct;
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (Correct)
            {
                // Send positive feedback to robot
                // Update game manager with correct answer to proceed with the questions
                Debug.Log("This is the correct answer");
            }
            else
            {
                // Send Negative feedback to robot
                Debug.Log("This is the wrong answer");
            }
        });
        StartCoroutine(SetButtonWidth(size));
    }
    public IEnumerator SetButtonWidth(Vector2 size)
    {
        yield return new WaitForEndOfFrame();
        myRect.sizeDelta = size;
    }
}
