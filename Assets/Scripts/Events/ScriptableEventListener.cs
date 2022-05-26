using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScriptableEventListener<T> : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private ScriptableEvent<T> _scriptableEvent;

    [Header("Relay Event")]
    [SerializeField] private UnityEvent<T> _relayEvent;

    private void Awake()
    {
        _scriptableEvent.Event.AddListener(Invoke);
    }

    private void Invoke(T variable)
    {
        _relayEvent.Invoke(variable);
    }
}
