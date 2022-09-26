using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteAlways]
public class BuildingForSale : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private BuildingInformation _buildingInformation;
    [SerializeField] private Image _shopIcon;
    [SerializeField] private TextMeshProUGUI _nameOfBuilding;
    [SerializeField] private TextMeshProUGUI _buildingDescription;
    [SerializeField] private ShopCostDisplay _costDisplay;
    [SerializeField] private ResourceTransaction _resourceTransaction;
    [SerializeField] private PurchaseBuilding _purchaseBuilding;

    [Header("Incrementation")]
    [SerializeField] private IncrementCurve _curve;

    [Header("Feedback")]
    [SerializeField] private int _numberOwned = 0;

    private void Awake()
    {
        _curve = GetComponent<IncrementCurve>();
    }
    private void Update()
    {
        _shopIcon.sprite = _buildingInformation.BuildingShopImage;
        _nameOfBuilding.text = _buildingInformation.BuildingName;
        _buildingDescription.text = _buildingInformation.BuildingDescription;
        _costDisplay.SetBuildingInfo(_buildingInformation);
        _resourceTransaction.SetBuildingInfo(_buildingInformation);
        _purchaseBuilding.SetBuildingInfo(_buildingInformation);
    }

    public void OnBuildingPurchase()
    {
        _numberOwned++;
        _curve.Increment(1);
        BuildingInformation updatedBuilding = Instantiate(_buildingInformation);
        updatedBuilding.ApplyCostMultiplier(_curve.ValueAtCurrentIncrement());
        _buildingInformation = updatedBuilding;
    }
}
