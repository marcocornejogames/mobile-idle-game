using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PurchaseBuilding : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private BuildingInformation _buildingToBePurchased;


    public void CompletePurchase()
    {
        BuildingPlacement.Instance.SetBuildingToBePlaced(_buildingToBePurchased);
    }

    public void SetBuildingInfo(BuildingInformation buildingInfo)
    {
        _buildingToBePurchased = buildingInfo;
    }
}
