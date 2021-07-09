using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Server = NetworkManager;
using MAKO = Topics;

public class SpatialModule : MonoBehaviour
{
    [System.Serializable]
    public class Introduction{
        public Sprite image;
        public string ttsText;
    }

    private bool isCorrect;


    [Header("Stage 1")]
    public GameObject imagesHolder;
    public GameObject introImg;
    public Introduction[] introImages;

    [Header("Stage 2")]
    public GameObject[] questions;

    [Header("Stage 3")]
    public GameObject stageThree;
 

    void Start()
    {
        isCorrect = false;
        initStage(PlayerPrefs.GetInt("SpatialSave", 1));
        //initStage(1);
        MAKO._topics.onCorrectAnswer += Correct;
        MAKO._topics.onWrongAnswer += Wrong;
    }

    void Update()
    {
        
    }

    void OnDestroy(){
        MAKO._topics.onCorrectAnswer -= Correct;
        MAKO._topics.onWrongAnswer -= Wrong;
    }

    // Wrapper functions
    void ResetState(){
        StartCoroutine(_ResetState());
    }

    void Say(string message){
        StartCoroutine(_Say(message));
    }

    private void initStage(int stage){
        StartCoroutine("Stage_" + stage);
    }

    private void Correct(){
        isCorrect = true;
        Say("Amazing!");
        ResetState();
    }

    private void Wrong(){
        isCorrect = false;
        Say("You can do better!");
    }

    IEnumerator Stage_1(){
        introImg.SetActive(true);
        Say("Today, we will play a fun game about prepositions.");
        while(!Server._Manager.ttsDone) yield return null;
        introImg.SetActive(false);
        imagesHolder.SetActive(true);
        foreach(Introduction introImage in introImages){
            if(introImage.image == null) continue;
            
            imagesHolder.GetComponent<Image>().sprite = introImage.image;
            Say(introImage.ttsText);
            while(!Server._Manager.ttsDone) yield return null;
            yield return new WaitForSeconds(introImage.ttsText.StartsWith("Look") || introImage.ttsText.StartsWith("Now") ? 2.5f : 1.5f);
        }
        yield return new WaitForSeconds(2);
        imagesHolder.SetActive(false);
        PlayerPrefs.SetInt("SpatialSave", 2);
        initStage(2);
    }

    IEnumerator Stage_2(){
        foreach(GameObject question in questions){
            question.SetActive(true);
            Say(question.GetComponent<QuestionMaker>().QuestionText);
            while(!isCorrect) yield return null;
            yield return new WaitForSeconds(1.5f);
            question.SetActive(false);
        }
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetInt("SpatialSave", 3);
        initStage(3);
    }

    IEnumerator Stage_3(){
        stageThree.SetActive(true);
        yield return null;
    }
    
    IEnumerator _Say(string message){
        Server._Manager.sendTTS(message);
        while(!Server._Manager.ttsDone) yield return null;
    }

    IEnumerator _ResetState(){
        yield return new WaitForSeconds(1);
        isCorrect = false;
    }
}
