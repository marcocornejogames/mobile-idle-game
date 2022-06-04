using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Control Broadcast Events")]
    [SerializeField] private OnVector2Event _touchOnePositionEvent;
    [SerializeField] private OnBoolEvent _touchOneIsDownEvent;
    [SerializeField] private OnVector2Event _touchTwoPositionEvent;
    [SerializeField] private OnBoolEvent _touchTwoIsDownEvent;

    //TOUCH 0
    private void OnTouch0(InputValue inputValue)
    {
        _touchOneIsDownEvent.Invoke(inputValue.isPressed);
    }

    private void OnTouch0Position(InputValue inputValue)
    {
        Vector2 touchPosition = inputValue.Get<Vector2>();
        _touchOnePositionEvent.Invoke(touchPosition);
    }

    //TOUCH 1
    private void OnTouch1(InputValue inputValue)
    {
        _touchTwoIsDownEvent.Invoke(inputValue.isPressed);
    }

    private void OnTouch1Position(InputValue inputValue)
    {
        Vector2 touchPosition = inputValue.Get<Vector2>();
        _touchTwoPositionEvent.Invoke(touchPosition);
    }
}
