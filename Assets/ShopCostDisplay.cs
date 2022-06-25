using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class ShopCostDisplay : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private GameObject _foodCostDisplay;
    [SerializeField] private TextMeshProUGUI _foodCostText;
    [SerializeField] private GameObject _woodCostDisplay;
    [SerializeField] private TextMeshProUGUI _woodCostText;
    [SerializeField] private GameObject _stoneCostDisplay;
    [SerializeField] private TextMeshProUGUI _stoneCostText;
    [SerializeField] private GameObject _knowledgeCostDisplay;
    [SerializeField] private TextMeshProUGUI _knowledgeCostText;
    [SerializeField] private GameObject _powerCostDisplay;
    [SerializeField] private TextMeshProUGUI _powerCostText;

    private BuildingInformation _buildingInformation;

    private void Update()
    {

        if (_buildingInformation == null) return;
        UpdateDisplay(_foodCostDisplay, _foodCostText, PlayerResourceManager.ResourceType.Food);
        UpdateDisplay(_woodCostDisplay, _woodCostText, PlayerResourceManager.ResourceType.Wood);
        UpdateDisplay(_stoneCostDisplay, _stoneCostText, PlayerResourceManager.ResourceType.Stone);
        UpdateDisplay(_knowledgeCostDisplay, _knowledgeCostText, PlayerResourceManager.ResourceType.Knowledge);
        UpdateDisplay(_powerCostDisplay, _powerCostText, PlayerResourceManager.ResourceType.Power);

    }

    private void UpdateDisplay(GameObject displayObject, TextMeshProUGUI costText, PlayerResourceManager.ResourceType typeOfResource)
    {
        int buildingCost = _buildingInformation.GetCost(typeOfResource);
        displayObject.SetActive(buildingCost > 0);
        costText.text = MathTools.ReadableNumber(buildingCost);
    }

    public void SetBuildingInfo(BuildingInformation _info)
    {
        _buildingInformation = _info;
    }
}
