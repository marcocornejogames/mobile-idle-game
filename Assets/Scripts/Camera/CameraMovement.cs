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
    [SerializeField] private Vector2 _mousePos;
    [SerializeField] private bool _leftButtonDown;
    [SerializeField] private float _scrollValue;


    [Header("Restraints")]
    [SerializeField] private bool _canMove = true;
    
    //Dragging and zooming logic variables
    private Vector3 dragOrigin;
    private bool isDragging = false;


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
            dragOrigin = _mainCamera.ScreenToWorldPoint(_mousePos);
            isDragging = false;
            return;
        }

        PanCamera();
        ZoomCamera();

    }

    //Camera Movement
    private void PanCamera()
    {
        if(_leftButtonDown && !isDragging)
        {
            //Debug.Log("Started dragging");
            dragOrigin = _mainCamera.ScreenToWorldPoint(_mousePos);
            isDragging = true;
        }

        if(isDragging)
        {
            Vector3 difference = dragOrigin - _mainCamera.ScreenToWorldPoint(_mousePos);
            _mainCamera.transform.position = ClampCamera(_mainCamera.transform.position + difference);
        }

        if (!_leftButtonDown) isDragging = false;
    }

    private void ZoomCamera()
    {
        if (_canMove) ApplyZoom(_scrollValue * _zoomFactor);
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
    public void UpdateMousePos(Vector2 position)
    {
        _mousePos = position;
    }

    public void UpdateLeftButton(bool isPressed)
    {
        _leftButtonDown = isPressed;
        //Debug.Log("Left pressed?" + isPressed);
    }

    public void UpdateScrollValue(Vector2 value)
    {
        _scrollValue = value.y;
    }

    public void SetCanMove(bool canMove)
    {
        _canMove = canMove;
    }
}
