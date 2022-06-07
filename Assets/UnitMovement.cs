using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Customization")]
    [SerializeField] private float _unitAcceleration = 2f;
    [SerializeField] private float _unitMaxSpeed = 3f;
    [SerializeField] private float _targetPositionMarginOfError = 0.1f;

    [Header("Feedback")]
    [SerializeField] private bool _hasTarget;

    //Coroutines
    private IEnumerator _movementCoroutine;


    //UNIT METHODS ___________________________________________
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }



    public void GoTo(Vector2 position)
    {
        _hasTarget = true;
        _movementCoroutine = Move(position);
        StartCoroutine(_movementCoroutine);
    }

    public void Stop()
    {
        _hasTarget = false;
        StopCoroutine(_movementCoroutine);
        StopMoving();
    }

    private IEnumerator Move(Vector2 targetPosition)
    {
        bool keepMoving = true;
        while(keepMoving)
        {
            if (CheckWithinMargin(targetPosition))
            {
                keepMoving = false;
                Stop();
            }


            MoveToPosition(targetPosition);

            yield return null;
        }    

    }

    private void MoveToPosition(Vector2 position)
    {
        Vector2 direction = position - new Vector2(transform.position.x, transform.position.y);
        Vector2 targetVelocity = _unitAcceleration * direction;

        if (_rigidbody.velocity.magnitude < _unitMaxSpeed) //Apply speed if not going too fast
        {
            _rigidbody.AddForce(targetVelocity);
        }
    }

    private void StopMoving()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    private bool CheckWithinMargin(Vector2 position)
    {
        float distance = Vector2.Distance(position, new Vector2(this.transform.position.x, this.transform.position.y));
        return (distance <= _targetPositionMarginOfError);
    }

    //________________________________Targetting
    public bool GetHasTarget()
    {
        return _hasTarget;
    }
}
