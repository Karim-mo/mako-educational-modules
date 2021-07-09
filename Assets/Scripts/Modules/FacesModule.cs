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

    private int i;


    [Header("Stage 1")]
    public Face[] faces;
    public GameObject facesPrefab;
    public GameObject demoHolder;
    public GameObject demoQuestion;

    public Face[] stageTwoFaces;
    public GameObject[] questions;
    
    [Header("Stage 2 & 3")]
    public GameObject stageTwo;
    public GameObject stageTwoHolderPrefab;

    [Header("Stage 4")]
    public GameObject stageFour;

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
        // Q1
        GameObject Holder = Instantiate(stageTwoHolderPrefab, stageTwo.transform);
        int max = i + 4;
        for(; i < max; i++){
            GameObject tempFace = Instantiate(facesPrefab, Holder.transform.GetChild(0).transform.GetChild(0).transform);
            tempFace.GetComponent<FaceWithName>().init(stageTwoFaces[i].img, stageTwoFaces[i].name);
        }
        StartCoroutine(Say("Get ready for the next question."));
        while(!isReady) yield return null;
        Destroy(Holder);
        questions[0].SetActive(true);
        while(!Server._Manager.ttsDone) yield return null;
        StartCoroutine(Say(questions[0].GetComponent<PictureQuestionMaker>().QuestionText));
        while(!isCorrect) yield return null;
        questions[0].SetActive(false);

        // Q2
        Holder = Instantiate(stageTwoHolderPrefab, stageTwo.transform);
        max = i + 4;
        for(; i < max; i++){
            GameObject tempFace = Instantiate(facesPrefab, Holder.transform.GetChild(0).transform.GetChild(0).transform);
            tempFace.GetComponent<FaceWithName>().init(stageTwoFaces[i].img, stageTwoFaces[i].name);
        }
        StartCoroutine(Say("Get ready for the next question."));
        while(!isReady) yield return null;
        Destroy(Holder);
        questions[1].SetActive(true);
        while(!Server._Manager.ttsDone) yield return null;
        StartCoroutine(Say(questions[1].GetComponent<PictureQuestionMaker>().QuestionText));
        while(!isCorrect) yield return null;
        questions[1].SetActive(false);

        // Q3
        Holder = Instantiate(stageTwoHolderPrefab, stageTwo.transform);
        max = i + 4;
        for(; i < max; i++){
            GameObject tempFace = Instantiate(facesPrefab, Holder.transform.GetChild(0).transform.GetChild(0).transform);
            tempFace.GetComponent<FaceWithName>().init(stageTwoFaces[i].img, stageTwoFaces[i].name);
        }
        StartCoroutine(Say("Get ready for the next question."));
        while(!isReady) yield return null;
        Destroy(Holder);
        questions[2].SetActive(true);
        while(!Server._Manager.ttsDone) yield return null;
        StartCoroutine(Say(questions[2].GetComponent<PictureQuestionMaker>().QuestionText));
        while(!isCorrect) yield return null;
        questions[2].SetActive(false);

        // Q4
        Holder = Instantiate(stageTwoHolderPrefab, stageTwo.transform);
        max = i + 4;
        for(; i < max; i++){
            GameObject tempFace = Instantiate(facesPrefab, Holder.transform.GetChild(0).transform.GetChild(0).transform);
            tempFace.GetComponent<FaceWithName>().init(stageTwoFaces[i].img, stageTwoFaces[i].name);
        }
        StartCoroutine(Say("Get ready for the next question."));
        while(!isReady) yield return null;
        Destroy(Holder);
        questions[3].SetActive(true);
        while(!Server._Manager.ttsDone) yield return null;
        StartCoroutine(Say(questions[3].GetComponent<PictureQuestionMaker>().QuestionText));
        while(!isCorrect) yield return null;
        questions[3].SetActive(false);

        // Q5
        Holder = Instantiate(stageTwoHolderPrefab, stageTwo.transform);
        max = i + 4;
        for(; i < max; i++){
            GameObject tempFace = Instantiate(facesPrefab, Holder.transform.GetChild(0).transform.GetChild(0).transform);
            tempFace.GetComponent<FaceWithName>().init(stageTwoFaces[i].img, stageTwoFaces[i].name);
        }
        StartCoroutine(Say("Get ready for the next question."));
        while(!isReady) yield return null;
        Destroy(Holder);
        questions[4].SetActive(true);
        while(!Server._Manager.ttsDone) yield return null;
        StartCoroutine(Say(questions[4].GetComponent<PictureQuestionMaker>().QuestionText));
        while(!isCorrect) yield return null;
        questions[4].SetActive(false);
        PlayerPrefs.SetInt("FacesSave", 3);
        Next(3);
    }

    IEnumerator Stage_3(){
        // Q6
        GameObject Holder = Instantiate(stageTwoHolderPrefab, stageTwo.transform);
        int max = i + 4;
        for(; i < max; i++){
            GameObject tempFace = Instantiate(facesPrefab, Holder.transform.GetChild(0).transform.GetChild(0).transform);
            tempFace.GetComponent<FaceWithName>().init(stageTwoFaces[i].img, stageTwoFaces[i].name);
        }
        StartCoroutine(Say("Get ready for the next question."));
        while(!isReady) yield return null;
        Destroy(Holder);
        questions[5].SetActive(true);
        while(!Server._Manager.ttsDone) yield return null;
        StartCoroutine(Say(questions[5].GetComponent<PictureQuestionMaker>().QuestionText));
        while(!isCorrect) yield return null;
        questions[5].SetActive(false);

        // Q7
        Holder = Instantiate(stageTwoHolderPrefab, stageTwo.transform);
        max = i + 4;
        for(; i < max; i++){
            GameObject tempFace = Instantiate(facesPrefab, Holder.transform.GetChild(0).transform.GetChild(0).transform);
            tempFace.GetComponent<FaceWithName>().init(stageTwoFaces[i].img, stageTwoFaces[i].name);
        }
        StartCoroutine(Say("Get ready for the next question."));
        while(!isReady) yield return null;
        Destroy(Holder);
        questions[6].SetActive(true);
        while(!Server._Manager.ttsDone) yield return null;
        StartCoroutine(Say(questions[6].GetComponent<PictureQuestionMaker>().QuestionText));
        while(!isCorrect) yield return null;
        questions[6].SetActive(false);

        // Q8
        Holder = Instantiate(stageTwoHolderPrefab, stageTwo.transform);
        max = i + 4;
        for(; i < max; i++){
            GameObject tempFace = Instantiate(facesPrefab, Holder.transform.GetChild(0).transform.GetChild(0).transform);
            tempFace.GetComponent<FaceWithName>().init(stageTwoFaces[i].img, stageTwoFaces[i].name);
        }
        StartCoroutine(Say("Get ready for the next question."));
        while(!isReady) yield return null;
        Destroy(Holder);
        questions[7].SetActive(true);
        while(!Server._Manager.ttsDone) yield return null;
        StartCoroutine(Say(questions[7].GetComponent<PictureQuestionMaker>().QuestionText));
        while(!isCorrect) yield return null;
        questions[7].SetActive(false);

        // Q9
        Holder = Instantiate(stageTwoHolderPrefab, stageTwo.transform);
        max = i + 4;
        for(; i < max; i++){
            GameObject tempFace = Instantiate(facesPrefab, Holder.transform.GetChild(0).transform.GetChild(0).transform);
            tempFace.GetComponent<FaceWithName>().init(stageTwoFaces[i].img, stageTwoFaces[i].name);
        }
        StartCoroutine(Say("Get ready for the next question."));
        while(!isReady) yield return null;
        Destroy(Holder);
        questions[8].SetActive(true);
        while(!Server._Manager.ttsDone) yield return null;
        StartCoroutine(Say(questions[8].GetComponent<PictureQuestionMaker>().QuestionText));
        while(!isCorrect) yield return null;
        questions[8].SetActive(false);

        // Q10
        Holder = Instantiate(stageTwoHolderPrefab, stageTwo.transform);
        max = i + 4;
        for(; i < max; i++){
            GameObject tempFace = Instantiate(facesPrefab, Holder.transform.GetChild(0).transform.GetChild(0).transform);
            tempFace.GetComponent<FaceWithName>().init(stageTwoFaces[i].img, stageTwoFaces[i].name);
        }
        StartCoroutine(Say("Get ready for the next question."));
        while(!isReady) yield return null;
        Destroy(Holder);
        questions[9].SetActive(true);
        while(!Server._Manager.ttsDone) yield return null;
        StartCoroutine(Say(questions[9].GetComponent<PictureQuestionMaker>().QuestionText));
        while(!isCorrect) yield return null;
        questions[9].SetActive(false);
        PlayerPrefs.SetInt("FacesSave", 4);
        Next(4);
    }

    IEnumerator Stage_4(){
        stageFour.SetActive(true);
        yield return null;
    }

    private void Correct(){
        isCorrect = true;
        StartCoroutine(Say("Amazing!"));
        StartCoroutine(ResetState());
    }

    private void Wrong(){
        isCorrect = false;
        StartCoroutine(Say("You can do better!"));
    }
    
    private void Ready(){
        isReady = true;
        StartCoroutine(Say("Great! Keep Going!"));
        StartCoroutine(ResetState());
    }
    
    private void Next(int stage){
        i = stage > 1 ? (stage - 2) * 20: 0;
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
