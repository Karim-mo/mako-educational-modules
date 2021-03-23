using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMaker : MonoBehaviour
{
    [System.Serializable]
    public class Answer
    {
        [TextArea()]
        public string AnswerText;
        public bool Correct;
    }
    [Header("Setup")]
    public GameObject Question;
    public GameObject rowHolderPrefab;
    public GameObject answerButtonPrefab;
    public GameObject AnswersHolder;

    [Header("Create your Question"), TextArea()]
    public string QuestionText;

    [Header("Create your Answers")]
    [Range(0, 5)]
    public int AnswersPerRow;
    public Answer[] Answers;


    
    void Start()
    {
        initQuestion();
    }

    void initQuestion()
    {
        Question.GetComponent<QuestionText>().initQuestionText(QuestionText);
        StartCoroutine(initUI());
    }

    IEnumerator initUI()
    {
        int len = Answers.Length;
        int k = 0;
        Vector2 max = new Vector2(-9999, -9999);
        foreach (var ans in Answers)
        {
            Vector3 pos = new Vector3(-9999, -9999);
            GameObject tempAnswer = Instantiate(answerButtonPrefab, pos, Quaternion.identity);
            tempAnswer.GetComponent<AnswerButton>().initButton(ans.AnswerText, ans.Correct);
            yield return new WaitForEndOfFrame();
            max.x = Mathf.Max(max.x, tempAnswer.GetComponent<RectTransform>().rect.width);
            max.y = Mathf.Max(max.y, tempAnswer.GetComponent<RectTransform>().rect.height);
            Destroy(tempAnswer);
        }


        for (int i = 0; i < Mathf.CeilToInt(len / (float)AnswersPerRow); i++)
        {
            GameObject tempRow = Instantiate(rowHolderPrefab, AnswersHolder.transform);
            int remain = len - k;
            for (int j = 0; j < Mathf.Min(AnswersPerRow, remain); j++)
            {
                GameObject tempAnswer = Instantiate(answerButtonPrefab, tempRow.transform);
                tempAnswer.GetComponent<AnswerButton>().initButton(Answers[k].AnswerText, Answers[k++].Correct, max);
            }
        }
    }
    
    void Update()
    {
        
    }
}
