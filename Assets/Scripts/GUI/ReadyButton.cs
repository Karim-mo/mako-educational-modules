using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyButton : MonoBehaviour
{
    public void Ready(){
        NetworkManager._Manager.sendServoExpression("left_up");
        Topics._topics.ReadyTrigger();
    }
}
