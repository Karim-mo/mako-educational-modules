using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Server = NetworkManager;

public class MovementUp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData data) {
        Server._Manager.sendMotorCommand("up");
    }

    public void OnPointerUp(PointerEventData data) {
        Server._Manager.sendMotorCommand("stop");
    }
}
