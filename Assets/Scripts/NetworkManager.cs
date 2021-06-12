using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager _Manager;

    class MakoServerMessage
    {
        public string type;
        public string message;
        public string exp_type;
        public string module_name;
    }


    [HideInInspector]
    public bool isConnected = false;
    [HideInInspector]
    public bool ttsDone;
    public bool emotionDone;
    [HideInInspector]
    public string sceneToLoad = "Null";

    Queue<MakoServerMessage> jobs;



    WebSocket ws;
    

    private void Awake()
    {
        if (_Manager != null)
        {
            Destroy(_Manager);
        }
        else
        {
            _Manager = this;
            DontDestroyOnLoad(this);
        }
        jobs = new Queue<MakoServerMessage>();
    }
    
    void Start()
    {
        ttsDone = true;
        ws = new WebSocket("ws://192.168.1.14:9000");
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log(e.Data);
            MakoServerMessage msg = JsonConvert.DeserializeObject<MakoServerMessage>(e.Data);
            jobs.Enqueue(msg);
            //Debug.Log(JsonConvert.DeserializeObject<LEDControlPacket>(e.Data).type);
            //Debug.Log("Message received from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        };
        StartCoroutine(Connect());
    }

    IEnumerator Connect()
    {
        while (!isConnected)
        {
            ws.Connect();
            yield return new WaitForSeconds(10);
        }
    }

    void Update()
    {
        while(jobs.Count > 0)
        {
            MakoServerMessage msg = jobs.Dequeue();
            if(msg.type == "welcome")
            {
                isConnected = true;
                //Debug.Log(isConnected);
            }
            if(msg.type == "module_request")
            {
                if (msg.message != "WaitMenu")
                {
                    MakoServerMessage _msg = new MakoServerMessage();
                    _msg.type = "module_response";
                    _msg.message = "Request Received";
                    _msg.module_name = msg.message;
                    ws.Send(JsonConvert.SerializeObject(_msg));
                }
                SceneManager.LoadScene(msg.message);
            }
            if(msg.type == "tts_response")
            {
                if(msg.message == "tts_complete"){
                    ttsDone = true;
                }
            }
            if(msg.type == "led_response")
            {
                if(msg.message == "led_complete"){
                    emotionDone = true;
                    Debug.Log(emotionDone);
                    Invoke("sendNeutralFace", 5);
                }
            }
        }
    }

    public void sendTTS(string message){
        ttsDone = false;
        MakoServerMessage msg = new MakoServerMessage();
        msg.type = "tts_request";
        msg.message = message;        
        ws.Send(JsonConvert.SerializeObject(msg));
    }

    public void sendExpression(string exp_type){
        emotionDone = false;
        MakoServerMessage msg = new MakoServerMessage();
        msg.type = "led_control";
        msg.exp_type = exp_type;        
        ws.Send(JsonConvert.SerializeObject(msg));
    }

    private void sendNeutralFace(){
        emotionDone = false;
        MakoServerMessage msg = new MakoServerMessage();
        msg.type = "led_control";
        msg.exp_type = "nf";        
        ws.Send(JsonConvert.SerializeObject(msg));
    }
}
