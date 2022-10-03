using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BuildingFeedbackData))]
public class HarvestSpawnBonus : MonoBehaviour, IInformational
{
    [Header("Component References")]
    [SerializeField] private BuildingFeedbackData _feedbackData;
    [SerializeField] private BuildingInformation _buildingInfo;

    [Header("Customization")]
    [SerializeField] private PlayerResourceManager.ResourceType _typeOfResourceBonus;
    [SerializeField] private int _bonusAmount = 1;
    [SerializeField] private float _spawnBonusRange = 10f;

    [Header("Feedback")]
    [SerializeField] private int _lifetimeBonus = 0;

    [Header("Events")]
    [SerializeField] private UnityEvent _onBonusGranted;

    private void Awake()
    {
        _feedbackData = GetComponent<BuildingFeedbackData>();
        _feedbackData.SetRadius(_spawnBonusRange);
    }
    public void RegisterHarvestOrSpawn(Harvestable harvestable)
    {
        if (Vector2.Distance(harvestable.transform.position, this.transform.position) > _spawnBonusRange) return; //If spawned too far away, ignore;

        PlayerResourceManager.Instance.AddToPlayerWallet(_bonusAmount, _typeOfResourceBonus);
        _lifetimeBonus += _bonusAmount;

        _onBonusGranted.Invoke();
    }

    //DISPLAY FEEDBACK______________________________________________________
    public void PopulateInformation(BuildingFeedbackDisplay feedbackDisplay)
    {
        _buildingInfo.BasicInformationDisplay(feedbackDisplay);
        feedbackDisplay.SetBody("Lifetime bonus: " + _lifetimeBonus + " " + _typeOfResourceBonus);
    }

    public GameObject GetObject()
    {
        return this.gameObject;
    }
}
