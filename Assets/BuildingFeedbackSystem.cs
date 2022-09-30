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


    //UNITY METHODS _______________________________
    private void Awake()
    {
        _circleRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        FindBuilding();
    }

    //BUILDING RADIUS FEEDBACK ______________________________________

    private void FindBuilding()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(_mousePos);
        List<Collider2D> allCollided = new List<Collider2D>();

        if(Physics2D.OverlapCircle(mouseWorldPos, 0.5f, _contactFilter, allCollided) > 0)
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
        _feedbackDisplay.gameObject.SetActive(true);

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(_mousePos);
        List<Collider2D> allCollided = new List<Collider2D>();

        if (Physics2D.OverlapCircle(mouseWorldPos, 0.5f, _contactFilter, allCollided) > 0) 
        {
            foreach (Collider2D collider in allCollided) // LOOK FOR BUILDINGS
            {
                if (collider.TryGetComponent<BuildingFeedbackData>(out BuildingFeedbackData building))
                {
                    DisplayBuildingInfo();
                    return;
                }
            }

            foreach (Collider2D collider in allCollided) // LOOK FOR GOBLIN
            {
                if (collider.TryGetComponent<GoblinBrain>(out GoblinBrain goblin))
                {
                    DisplayGoblinInfo();
                    return;
                }
            }
        }

        _feedbackDisplay.gameObject.SetActive(false);
    }


    private void DisplayBuildingInfo()
    {

    }

    private void DisplayGoblinInfo()
    {

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


}
