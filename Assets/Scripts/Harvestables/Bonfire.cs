using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildingFeedbackData))]
public class Bonfire : MonoBehaviour, IInformational
{
    [Header("Component References")]
    [SerializeField] private BuildingFeedbackData _feedbackData;
    [SerializeField] private BuildingInformation _buildingInfo;

    [Header("Customization")]
    [SerializeField] private float _speedBonus = 0.1f;
    [SerializeField] private bool _limitedTime = false;
    [SerializeField] private float _lifeSpan = 60f;
    [SerializeField] private float _range = 1f;

    private int _lifetimeBonus;


    private void Awake()
    {
        if(_limitedTime) Invoke("Die", _lifeSpan);

        _feedbackData = GetComponent<BuildingFeedbackData>();
        _feedbackData.SetRadius(_range);
    }

    public void RegisterSpawn(GoblinBrain goblin)
    {
        if (Vector2.Distance(this.transform.position, goblin.transform.position) > _range) return;
        UnitMovement goblinMovement = goblin.GetUnitMovement();
        goblinMovement.BoostSpeed(_speedBonus);

        _lifetimeBonus++;
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    //DISPLAY FEEDBACK______________________________________________________
    public void PopulateInformation(BuildingFeedbackDisplay feedbackDisplay)
    {
        _buildingInfo.BasicInformationDisplay(feedbackDisplay);
        feedbackDisplay.SetBody("Fuel the fire to fuel the goblin rage within! \n Lifetime bonus: " + _lifetimeBonus + " goblins.");
    }

    public GameObject GetObject()
    {
        return this.gameObject;
    }
}
