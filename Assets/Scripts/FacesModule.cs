using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Server = NetworkManager;
using MAKO = Topics;

public class FacesModule : MonoBehaviour
{
    [System.Serializable]
    public class Face {
        public Sprite img;
        public string name;
    }

    private bool isCorrect;
    private bool isReady;

    [Header("Stage 1")]
    public Face[] faces;
    public GameObject facesPrefab;
    public GameObject demoHolder;
    public GameObject demoQuestion;

    [Header("Stage 2")]
    public GameObject stageTwo;
    public Face[] stageTwoFaces;
    public GameObject stageTwoHolderPrefab;
    public GameObject questionOne;
    public GameObject questionTwo;
    public GameObject questionThree;
    public GameObject questionFour;
    public GameObject questionFive;

    void Start()
    {
        isCorrect = false;
        isReady = false;
        MAKO._topics.onCorrectAnswer += Correct;
        MAKO._topics.onWrongAnswer += Wrong;
        MAKO._topics.onReadyTrigger += Ready;
        Next(PlayerPrefs.GetInt("FacesSave", 1));
    }

    IEnumerator Stage_1(){
        Server._Manager.sendTTS("We are going to see how well you can remember these people.");
        while(!Server._Manager.ttsDone) yield return null;
        Server._Manager.sendTTS("Let's talk about how will this game work.");
        while(!Server._Manager.ttsDone) yield return null;
        foreach(Face face in faces){
            GameObject tempFace = Instantiate(facesPrefab, demoHolder.transform);
            tempFace.GetComponent<FaceWithName>().init(face.img, face.name);
        }
        Server._Manager.sendTTS("Here are 4 pictures of people and their faces.");
        while(!Server._Manager.ttsDone) yield return null;
        Server._Manager.sendTTS("Try to remember each person.");
        while(!Server._Manager.ttsDone) yield return null;
        Server._Manager.sendTTS("Then you will be asked a question like this.");
        while(!Server._Manager.ttsDone) yield return null;
        demoHolder.SetActive(false);
        demoQuestion.SetActive(true);
        yield return new WaitForSeconds(2);
        Server._Manager.sendTTS(demoQuestion.GetComponent<PictureQuestionMaker>().QuestionText);
        while(!Server._Manager.ttsDone) yield return null;
        yield return new WaitForSeconds(1);
        Server._Manager.sendTTS("Hint: It is the last choice.");
        while(!Server._Manager.ttsDone) yield return null;
        while(!isCorrect) yield return null;
        PlayerPrefs.SetInt("FacesSave", 2);
        demoQuestion.SetActive(false);
        Next(2);
    }

    IEnumerator Stage_2(){
        int i = 0;
        GameObject Holder = Instantiate(stageTwoHolderPrefab, stageTwo.transform);
        int max = i + 4;
        for(; i < max; i++){
            GameObject tempFace = Instantiate(facesPrefab, Holder.transform.GetChild(0).transform.GetChild(0).transform);
            tempFace.GetComponent<FaceWithName>().init(stageTwoFaces[i].img, stageTwoFaces[i].name);
        }
        StartCoroutine(Say("Get ready for the next question."));
        while(!isReady) yield return null;
        Destroy(Holder);
        questionOne.SetActive(true);
        StartCoroutine(Say(questionOne.GetComponent<PictureQuestionMaker>().QuestionText));
        while(!isCorrect) yield return null;
        questionOne.SetActive(false);
    }

    IEnumerator Stage_3(){
        yield return null;
    }

    private void Correct(){
        isCorrect = true;
        StartCoroutine(Say("Amazing!"));
        StartCoroutine(ResetState());
    }

    private void Wrong(){
        isCorrect = false;
        StartCoroutine(Say("You can do better! I believe in you, friend."));
    }
    
    private void Ready(){
        isReady = true;
        StartCoroutine(Say("Great! Keep Going!"));
        StartCoroutine(ResetState());
    }
    
    private void Next(int stage){
        StartCoroutine("Stage_" + stage);
    }

    IEnumerator Say(string message){
        Server._Manager.sendTTS(message);
        while(!Server._Manager.ttsDone) yield return null;
    }

    IEnumerator ResetState(){
        yield return new WaitForSeconds(2);
        isCorrect = false;
        isReady = false;
    }

    private void OnDestroy(){
        MAKO._topics.onCorrectAnswer -= Correct;
        MAKO._topics.onWrongAnswer -= Wrong;
        MAKO._topics.onReadyTrigger -= Ready;
    }
}
