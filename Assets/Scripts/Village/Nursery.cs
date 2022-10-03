using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildingFeedbackData))]
public class Nursery : MonoBehaviour, IInformational
{
    [Header("Customization")]
    [SerializeField] private float _lifespanBonus = 0.5f;
    [SerializeField] private float _range = 5f;

    [Header("Component References")]
    [SerializeField] private BuildingFeedbackData _feedbackData;
    [SerializeField] private BuildingInformation _buildingInfo;

    private int _lifetimeBonus;
    public void RegisterSpawn(GoblinBrain goblin)
    {
        if (Vector2.Distance(this.transform.position, goblin.transform.position) > _range) return;
        float currentLifespan = goblin.GetLifespan();
        goblin.SetUnitLifespan(currentLifespan *= (1 +_lifespanBonus));

        _lifetimeBonus++;
    }

    private void Awake()
    {
        _feedbackData = GetComponent<BuildingFeedbackData>();
        _feedbackData.SetRadius(_range);
    }

    //DISPLAY FEEDBACK______________________________________________________
    public void PopulateInformation(BuildingFeedbackDisplay feedbackDisplay)
    {
        _buildingInfo.BasicInformationDisplay(feedbackDisplay);
        feedbackDisplay.SetBody("Yes, goblins breast feed. Deal with it.\n" + "Lifetime bonus: " + _lifetimeBonus + " goblins.");
    }

    public GameObject GetObject()
    {
        return this.gameObject;
    }
}
