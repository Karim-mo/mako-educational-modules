using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Server = NetworkManager;


public class MovementLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData data) {
        Server._Manager.sendMotorCommand("left");
    }

    public void OnPointerUp(PointerEventData data) {
        Server._Manager.sendMotorCommand("stop");
    }
}