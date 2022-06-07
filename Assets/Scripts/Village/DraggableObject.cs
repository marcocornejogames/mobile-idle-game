using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DraggableObject : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Controls")]
    [SerializeField] private InputActionAsset _actionAsset;
    [SerializeField] private string _onTouchHoldActionName = "Touch0Hold";
    [SerializeField] private string _touchPositionActionName = "Touch0Position";

    [Header("Feedback")]
    [SerializeField] private bool _canDrag = true;
    [SerializeField] private bool _isDragging;

    [Header("Events")]
    [SerializeField] private OnBoolEvent _freezeCameraEvent;


    private void Awake()
    {
        _actionAsset.FindAction(_onTouchHoldActionName).performed += OnTouchHold;
        _actionAsset.FindAction(_onTouchHoldActionName).canceled += OnTouchHoldRelease;

        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDragging)
        {
            DragObject();
        }
    }

    private void DragObject()
    {
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(_actionAsset.FindAction(_touchPositionActionName).ReadValue<Vector2>());
        _rigidbody.MovePosition(new Vector3(touchPosition.x, touchPosition.y, _rigidbody.transform.position.z));
    }

    //Events 
    private void OnTouchHold(InputAction.CallbackContext action)
    {
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(_actionAsset.FindAction(_touchPositionActionName).ReadValue<Vector2>());
        bool isTouching = _collider.bounds.Contains(new Vector3(touchPosition.x, touchPosition.y, _collider.transform.position.z));

        if (isTouching && _canDrag)
        {
            _freezeCameraEvent.Invoke(false);
            _isDragging = true;
        }
    }

    private void OnTouchHoldRelease(InputAction.CallbackContext action)
    {
        Debug.Log("Release");
        if (_isDragging)
        {
            _freezeCameraEvent.Invoke(true);
            _isDragging = false;
        }

    }

}
