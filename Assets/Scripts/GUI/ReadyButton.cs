using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyButton : MonoBehaviour
{
    public void Ready(){
        Topics._topics.ReadyTrigger();
    }
}
