using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Control Broadcast Events")]
    [SerializeField] private OnVector2Event _mousePosEvent;
    [SerializeField] private OnBoolEvent _onRightClickEvent;
    [SerializeField] private OnBoolEvent _onLeftClickEvent;
    [SerializeField] private OnVector2Event _onScrollEvent;

    private void OnRightClick(InputValue inputValue)
    {
        _onRightClickEvent.Invoke(inputValue.isPressed);
        //Debug.Log(RB Down = " + inputValue.isPressed);
    }

    private void OnMousePosition(InputValue inputValue)
    {
        Vector2 mousPos = inputValue.Get<Vector2>();
        _mousePosEvent.Invoke(mousPos);
        //Debug.Log("Mouse Pos = " + mousPos);
    }

    private void OnLeftClick(InputValue inputValue)
    {
        _onLeftClickEvent.Invoke(inputValue.isPressed);
        //Debug.Log("LMB Down = " + inputValue.isPressed);
    }

    private void OnScroll(InputValue inputValue)
    {
        Vector2 scrollValue = inputValue.Get<Vector2>();
        _onScrollEvent.Invoke(scrollValue);
        //Debug.Log("Scroll Value = " + scrollValue);
    }
}
