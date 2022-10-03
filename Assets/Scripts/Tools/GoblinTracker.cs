using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinTracker : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] private int _numberOfGoblins;

    [Header("Component References")]
    [SerializeField] private OnFloatEvent _updateNumberEvent;

    public void AddGoblin(GoblinBrain goblin)
    {
        _numberOfGoblins++;
    }

    public void SubGoblin(GoblinBrain goblin)
    {
        _numberOfGoblins--;
    }

    private void Update()
    {
        _updateNumberEvent.Invoke(_numberOfGoblins);
    }
}
