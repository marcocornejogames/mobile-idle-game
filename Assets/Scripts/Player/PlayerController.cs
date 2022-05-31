using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Control Broadcast Events")]
    [SerializeField] private OnVector2Event _touchOnePositionEvent;
    [SerializeField] private OnBoolEvent _touchOneIsDownEvent;
    private void OnTouch0(InputValue inputValue)
    {
        _touchOneIsDownEvent.Invoke(inputValue.isPressed);
    }

    private void OnTouch0Position(InputValue inputValue)
    {
        Vector2 touchPosition = inputValue.Get<Vector2>();
        _touchOnePositionEvent.Invoke(touchPosition);
    }
}
