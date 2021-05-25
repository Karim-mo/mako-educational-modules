using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionText : MonoBehaviour
{
    public RectTransform imageRect;
    public TextMeshProUGUI questionText;
    private RectTransform myRect;

    private void Start()
        => myRect = GetComponent<RectTransform>();
    
    public void initQuestionText(string text)
    {
        questionText.text = text;
        StartCoroutine(SetBGWidth());
    }
    public IEnumerator SetBGWidth()
    {
        yield return new WaitForEndOfFrame();
        imageRect.sizeDelta = new Vector2(myRect.rect.width + 20, myRect.sizeDelta.y + 10);
        imageRect.gameObject.SetActive(true);
    }
}
