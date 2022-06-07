using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBrain : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private UnitMovement _unitMovement;

    [Header("Idle")]
    [SerializeField] private float _idleMovementRange = 2f;
    [SerializeField] private float _idleWaitingTimeMIN = 3f;
    [SerializeField] private float _idleWaitingTimeMAX = 5f;
    [Range(0, 1)] [SerializeField] private float _chanceOfIdleWaiting = 0.5f;


    [Header("Feedback")]
    [SerializeField] private GoblinState _currentState = GoblinState.Idle;


    private bool _isWaiting;


    //Enums
    private enum GoblinState
    {
        Idle,
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
            Invoke("StopWaiting", Random.Range(_idleWaitingTimeMIN, _idleWaitingTimeMAX));
        }

        if(!_unitMovement.GetHasTarget() && !_isWaiting) //Is idling but not moving around right now
        {
            Vector2 newTargetPosition = MathTools.FindVector2WithinRange(transform.position, _idleMovementRange);
            if (VillageBoundaries.Instance.IsInsideBounds(newTargetPosition)) _unitMovement.GoTo(newTargetPosition);
            else _unitMovement.Stop();
        }
    }

    private void StopWaiting()
    {
        _isWaiting = false;
    }

}
