using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Camera _mainCamera;

    [Header("Feedback")]
    [SerializeField] private Vector2 _touchOnePosition;
    [SerializeField] private bool _touchOneIsPressed;
    
    private Vector3 dragOrigin;
    private bool isDragging = false;

    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
    }
    private void Update()
    {
        PanCamera();
    }

    //Camera Movement
    private void PanCamera()
    {
        if(_touchOneIsPressed && !isDragging)
        {
            dragOrigin = _mainCamera.ScreenToWorldPoint(_touchOnePosition);
            isDragging = true;
        }

        if(isDragging)
        {
            Vector3 difference = dragOrigin - _mainCamera.ScreenToWorldPoint(_touchOnePosition);
            _mainCamera.transform.position += difference;
        }

        if (!_touchOneIsPressed) isDragging = false;
    }

    //EVENT LISTENERS _________________________________________________
    public void UpdateTouchOnePosition(Vector2 position)
    {
        _touchOnePosition = position;
    }

    public void UpdateTouchOneIsPressed(bool isPressed)
    {
        _touchOneIsPressed = isPressed;
    }
}
