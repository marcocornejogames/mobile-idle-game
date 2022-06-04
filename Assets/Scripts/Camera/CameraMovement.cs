using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private SpriteRenderer _backgroundRenderer;

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

    [Header("Restraints")]
    [SerializeField] private bool _canMove = true;
    
    //Dragging and zooming logic variables
    private Vector3 dragOrigin;
    private bool isDragging = false;
    private float startPinchDistance;
    private bool isZooming = false;

    //Background 
    private float _backgroundMinX;
    private float _backgroundMaxX;
    private float _backgroundMinY;
    private float _backgroundMaxY;


    private void Awake()
    {
        //Find components
        _mainCamera = GetComponent<Camera>();


        //Assign background variables
        _backgroundMinX = _backgroundRenderer.transform.position.x - _backgroundRenderer.bounds.size.y / 2;
        _backgroundMinY = _backgroundRenderer.transform.position.y - _backgroundRenderer.bounds.size.y / 2;
        _backgroundMaxX = _backgroundRenderer.transform.position.x + _backgroundRenderer.bounds.size.x / 2;
        _backgroundMaxY = _backgroundRenderer.transform.position.y + _backgroundRenderer.bounds.size.y / 2;


    }
    private void Update()
    {
        if(!_canMove)
        {
            dragOrigin = _mainCamera.ScreenToWorldPoint(_touchOnePosition);
            isDragging = false;
            return;
        }

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
            _mainCamera.transform.position = ClampCamera(_mainCamera.transform.position + difference);
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

        if (isZooming && _canMove)
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
        _mainCamera.transform.position = ClampCamera(_mainCamera.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = _mainCamera.orthographicSize;
        float camWidth = _mainCamera.orthographicSize * _mainCamera.aspect;

        float minX = _backgroundMinX + camWidth;
        float maxX = _backgroundMaxX - camWidth;

        float minY = _backgroundMinY + camHeight;
        float maxY = _backgroundMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);

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

    public void SetCanMove(bool canMove)
    {
        _canMove = canMove;
    }
}
