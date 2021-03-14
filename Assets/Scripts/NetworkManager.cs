using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;

public class NetworkManager : MonoBehaviour
{ 
    class LEDControlPacket
    {
        public string type;
        public string exp_type;
    }
    WebSocket ws;
    public static NetworkManager _Manager;

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
    }
    
    void Start()
    {
        ws = new WebSocket("ws://192.168.1.14:9000");
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log(e.Data);
            //Debug.Log(JsonConvert.DeserializeObject<LEDControlPacket>(e.Data).type);
            //Debug.Log("Message recived from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        };

        ws.Connect();

    }

    void Update()
    {
        
    }
}
