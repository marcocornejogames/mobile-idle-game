using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBrain : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private UnitMovement _unitMovement;

    [Header("Customization")]
    [SerializeField] private float _idleMovementRange = 2f;
    [SerializeField] private float _idleWaitingTime = 3f;
    [Range(0, 1)] [SerializeField] private float _chanceOfIdleWaiting = 0.5f;

    [Header("Feedback")]
    [SerializeField] private GoblinState _currentState = GoblinState.Idle;

    private bool _isWaiting;


    //Enums
    private enum GoblinState
    {
        Idle
    }

    // UNIT METHODS ______________________________________________________
    private void Awake()
    {
        _unitMovement = GetComponent<UnitMovement>();
    }

    private void Update()
    {
        switch (_currentState)
        {
            case GoblinState.Idle:
                Idle();
                break;
            default:
                break;
        }
    }

    // CUSTOM METHODS __________________________________________________

    //____________________________________________________STATE MACHINE
    private void Idle()
    {
        if(!_isWaiting && Random.value <= _chanceOfIdleWaiting) //Chance to just wait for a bit
        {
            _isWaiting = true;
            Invoke("StopWaiting", _idleWaitingTime);
        }

        if(!_unitMovement.GetHasTarget() && !_isWaiting) //Is idling but not moving around right now
        {
            _unitMovement.GoTo(MathTools.FindVector2WithinRange(transform.position, _idleMovementRange));
        }
    }

    private void StopWaiting()
    {
        _isWaiting = false;
    }

    //COROUTINES __________________________________________________

}
