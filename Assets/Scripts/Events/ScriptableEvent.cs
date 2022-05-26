using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScriptableEvent<T> : ScriptableObject
{
    public UnityEvent<T> Event;
    public void Invoke(T variable)
    {
        Event.Invoke(variable);
    }
}
