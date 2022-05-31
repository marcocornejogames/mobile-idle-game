using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Camera _mainCamera;

    [Header("Customization")]
    [SerializeField] private float _minZoom = 3f;
    [SerializeField] private float _maxZoom = 10f;
    [SerializeField] private float _zoomFactor = 0.5f;

    [Header("Feedback")]
    [SerializeField] private Vector2 _touchOnePosition;
    [SerializeField] private bool _touchOneIsPressed;
    [SerializeField] private Vector2 _touchTwoPosition;
    [SerializeField] private bool _touchTwoIsPressed;
    [SerializeField] private float _currentPinchDistance;
    
    private Vector3 dragOrigin;
    private bool isDragging = false;
    private float startPinchDistance;
    private bool isZooming = false;

    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
    }
    private void Update()
    {
        PanCamera();
        ZoomCamera();
    }

    //Camera Movement
    private void PanCamera()
    {
        if(_touchOneIsPressed && !isDragging && !_touchTwoIsPressed)
        {
            dragOrigin = _mainCamera.ScreenToWorldPoint(_touchOnePosition);
            isDragging = true;
        }

        if(isDragging && !_touchTwoIsPressed)
        {
            Vector3 difference = dragOrigin - _mainCamera.ScreenToWorldPoint(_touchOnePosition);
            _mainCamera.transform.position += difference;
        }

        if (!_touchOneIsPressed || _touchTwoIsPressed) isDragging = false;
    }

    private void ZoomCamera()
    {
        if (_touchOneIsPressed && _touchTwoIsPressed && !isZooming)
        {
            startPinchDistance = (_touchOnePosition - _touchTwoPosition).magnitude;
            isZooming = true;
        }

        if (isZooming)
        {
            _currentPinchDistance = (_touchOnePosition - _touchTwoPosition).magnitude;
            float pinchDistanceDifference = _currentPinchDistance - startPinchDistance;
            ApplyZoom(pinchDistanceDifference * _zoomFactor);
        }

        if (!_touchOneIsPressed || !_touchTwoIsPressed) isZooming = false;
    }

    private void ApplyZoom(float increment)
    {
        _mainCamera.orthographicSize = Mathf.Clamp(_mainCamera.orthographicSize - increment, _minZoom, _maxZoom);
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

    public void UpdateTouchTwoPosition(Vector2 position)
    {
        _touchTwoPosition = position;
    }

    public void UpdateTouchTwoIsPressed(bool isPressed)
    {
        _touchTwoIsPressed = isPressed;
    }
}
