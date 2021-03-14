using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;


public class network : MonoBehaviour
{
    class LEDControlPacket
    {
        public string type;
        public string exp_type;
    }

    WebSocket ws;
    bool flag = false;

    void Start()
    {
        ws = new WebSocket("ws://192.168.1.8:9000");
        ws.OnMessage += (sender, e) =>
        {

            Debug.Log("Message recived from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        };

        ws.Connect();



    }


    // Update is called once per frame
    void Update()
    {
        if (ws == null || flag)
        {
            return;
        }
        LEDControlPacket packet = new LEDControlPacket();
        packet.type = "led_control";
        packet.exp_type = "hf";
        ws.Send(JsonConvert.SerializeObject(packet));
        flag = true;
    }

}
