using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFeedbackSystem : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private LineRenderer _circleRenderer;
    [SerializeField] private BuildingFeedbackDisplay _feedbackDisplay;

    [Header("Customization")]
    [SerializeField] private int _numberOfSteps = 100;
    [SerializeField] private ContactFilter2D _contactFilter;

    [Header("Feedback")]
    [SerializeField] private Vector2 _mousePos;
    private IInformational _currentFeedbackData;
    private GameObject _feedbackObject;

    private bool _canDisplay = true;
    //UNITY METHODS _______________________________
    private void Awake()
    {
        _circleRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        FindBuilding();

        if (_feedbackObject != null && _currentFeedbackData != null) DisplayInformation(_currentFeedbackData);
        else _feedbackDisplay.gameObject.SetActive(false);
    }

    //BUILDING RADIUS FEEDBACK ______________________________________

    private void FindBuilding()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(_mousePos);
        List<Collider2D> allCollided = new List<Collider2D>();

        if(Physics2D.OverlapCircle(mouseWorldPos, 0.01f, _contactFilter, allCollided) > 0)
        {
            foreach (Collider2D collider in allCollided)
            {
                if(collider.TryGetComponent<BuildingFeedbackData>(out BuildingFeedbackData building))
                {
                    DrawCircle(building.GetRadius(), building.transform.position);
                    return;
                }
            }
        }

        EraseCircle();

    }

    private void DrawCircle(float radius, Vector2 centre)
    {
        _circleRenderer.enabled = true;

        _circleRenderer.positionCount = _numberOfSteps;

        for (int currentStep = 0; currentStep < _numberOfSteps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / _numberOfSteps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = (xScaled * radius) + centre.x ;
            float y = (yScaled * radius) + centre.y ;

            Vector3 currentPosition = new Vector3(x, y, transform.position.z);

            _circleRenderer.SetPosition(currentStep, currentPosition);
        }
    }

    private void EraseCircle()
    {
        _circleRenderer.enabled = false;
    }

    //FEEDBACK DISPLAY ___________________________________________________

    private void TryDisplayInfo()
    {
        if (!_canDisplay)
        {
            _feedbackDisplay.gameObject.SetActive(false);
            return;
        }

        _feedbackDisplay.gameObject.SetActive(true);

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(_mousePos);
        List<Collider2D> allCollided = new List<Collider2D>();

        if (Physics2D.OverlapCircle(mouseWorldPos, 0.5f, _contactFilter, allCollided) > 0) 
        {
            foreach (Collider2D collider in allCollided) // LOOK FOR BUILDINGS
            {
                if (collider.TryGetComponent<IInformational>(out IInformational subject))
                {
                    _currentFeedbackData = subject;
                    _feedbackObject = _currentFeedbackData.GetObject();
                    return;
                }
            }

        }

        _feedbackDisplay.gameObject.SetActive(false);
    }


    private void DisplayInformation(IInformational subject)
    {
        subject.PopulateInformation(_feedbackDisplay);
    }

    //UPDATE PLAYER INPUT _____________________________________________
    public void UpdateMousePosition(Vector2 mousePos)
    {
        _mousePos = mousePos;
    }

    public void OnClick(bool isDown)
    {
        if (isDown) TryDisplayInfo();
    }

        public GameObject GetObject()
    {
        return this.gameObject;
    }

    //TOGGLE________________________________________________________
    public void SetCanDisplay(bool canDisplay)
    {
        _canDisplay = canDisplay;
    }
}
