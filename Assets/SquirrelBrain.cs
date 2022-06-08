using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelBrain : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] UnitMovement _unitMovement;

    [Header("Customization")]
    [SerializeField] private float _wanderingRange = 3f;
    [Range(0, 1)] [SerializeField] private float _chanceOfIdleWaiting = 0.5f;
    [SerializeField] private float _waitingTimeMIN = 1f;
    [SerializeField] private float _waitingTimeMAX = 2f;

    private bool _isWaiting;

    private void Awake()
    {
        _unitMovement = GetComponent<UnitMovement>();
    }

    private void Update()
    {

        if (!_isWaiting && Random.value <= _chanceOfIdleWaiting) //Chance to just wait for a bit
        {
            _isWaiting = true;
            Invoke("StopWaiting", Random.Range(_waitingTimeMIN, _waitingTimeMAX));
        }

        if (!_unitMovement.GetHasTarget() && !_isWaiting) //Is idling but not moving around right now
        {
            Vector2 newTargetPosition = MathTools.FindVector2WithinRange(transform.position, _wanderingRange);
            if (VillageBoundaries.Instance.IsInsideBounds(newTargetPosition)) _unitMovement.GoTo(newTargetPosition);
            else _unitMovement.Stop();
        }
    }

    private void StopWaiting()
    {
        _isWaiting = false;
    }
}
