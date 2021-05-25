using System;
using UnityEngine;

public class Topics : MonoBehaviour
{
    public static Topics _topics;

     private void Awake()
    {
        if (_topics != null)
        {
            Destroy(_topics);
        }
        else
        {
            _topics = this;
            DontDestroyOnLoad(this);
        }
    }

    public event Action onCorrectAnswer;
    public void CorrectAnswerTrigger() => onCorrectAnswer?.Invoke();
        
    public event Action onWrongAnswer;
    public void WrongAnswerTrigger() => onWrongAnswer?.Invoke();
    
    public event Action onReadyTrigger;
    public void ReadyTrigger() => onReadyTrigger?.Invoke();


}
