using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureQuestionMaker : MonoBehaviour
{
    [System.Serializable]
    public class Answer
    {
        public Sprite image;
        public bool Correct;
    }
    [Header("Setup")]
    public GameObject Question;
    public GameObject answerImagePrefab;
    public GameObject rowHolder;

    [Header("Create your Question"), TextArea()]
    public string QuestionText;

    [Header("Create your Answers")]
    public Answer[] Answers;

    void Start()
    {
        if(Answers.Length > 4)
        {
            Debug.LogError("Picture answer array size cannot exceed 4.");
            return;
        }
        initUI();
    }

    void initUI()
    {
        Question.GetComponent<QuestionText>().initQuestionText(QuestionText);

        int len = Answers.Length;
        for (int i = 0; i < Mathf.Min(4, len); i++)
        {
            GameObject tempAnswer = Instantiate(answerImagePrefab, rowHolder.transform);
            tempAnswer.GetComponent<AnswerPicture>().initButton(Answers[i].image, Answers[i].Correct);
        }
    }
}
