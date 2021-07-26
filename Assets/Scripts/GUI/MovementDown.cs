using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Server = NetworkManager;


public class MovementDown : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData data) {
        Server._Manager.sendMotorCommand("down");
    }

    public void OnPointerUp(PointerEventData data) {
        Server._Manager.sendMotorCommand("stop");
    }
}