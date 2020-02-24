using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ObjectiveStateChange : MonoBehaviour
{
    public UnityEvent OnTriggered;
    
    public void FireEvent()
    {
        OnTriggered.Invoke();
    }
}
