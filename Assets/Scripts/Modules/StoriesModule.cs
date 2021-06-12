using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Server = NetworkManager;
using MAKO = Topics;

public class StoriesModule : MonoBehaviour
{
    public Sprite[] stories;
    [TextArea()]
    public string storiesText;

    public Image storyImage;

    private int page;
    private string[] ttsText;

    private bool faded = true;
    void Start()
    {
        page = PlayerPrefs.GetInt("StoriesPage", 0);
        ttsText = storiesText.Split('\n');
        storyImage.sprite = stories[page];
        // foreach(string s in ttsText){
        //     Debug.Log(s);
        // }
    }

    void Update()
    {
        // storyImage.sprite = stories[page];
    }

    public void nextPage(){
        if (!faded || page >= stories.Length - 1) return;
        page++;
        StartCoroutine(fadeOut());
        PlayerPrefs.SetInt("StoriesPage", page);
    }

    public void prevPage(){
        if (!faded || page <= 0) return;
        page--;
        StartCoroutine(fadeOut());
        PlayerPrefs.SetInt("StoriesPage", page);
    }

    public void Narrate(){
        StartCoroutine(_Say(ttsText[page]));
    }

    IEnumerator _Say(string message){
        Server._Manager.sendTTS(message);
        while(!Server._Manager.ttsDone) yield return null;
    }

    IEnumerator fadeOut(){
        faded = false;
        for (float i = 1f; i >= 0; i -= 0.05f)
        {
            Color temp = storyImage.color;
            temp.a = i;
            storyImage.color = temp;
            yield return new WaitForSeconds(0.01f);
        }
        storyImage.sprite = stories[page];
        StartCoroutine(fadeIn());
        yield return null;
    }

    IEnumerator fadeIn(){
        for (float i = storyImage.color.a; i <= 1; i += 0.05f)
        {
            Color temp = storyImage.color;
            temp.a = i;
            storyImage.color = temp;
            yield return new WaitForSeconds(0.01f);
        }
        faded = true;
        yield return null;
    }
}
