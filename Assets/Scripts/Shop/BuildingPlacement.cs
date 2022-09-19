using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class BuildingPlacement : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Image _buildingPlacementImage;
    [SerializeField] private TextMeshProUGUI _nameOfBuilding;
    [SerializeField] private InputActionAsset _input;
    [SerializeField] private string _placeBuildingKeyName = "Click";
    [SerializeField] private string _touchPositionKeyName = "MousePosition";

    [Header("Feedback")]
    [SerializeField] private BuildingInformation _buildingInformation;

    [Header("Events")]
    [SerializeField] private UnityEvent _onBuildingPlaced;

    public static BuildingPlacement Instance;

    private void Awake()
    {
        //Manage Instances
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        _input.FindAction(_placeBuildingKeyName).performed += PlaceBuilding;
    }

    public void SetBuildingToBePlaced(BuildingInformation building)
    {
        _buildingInformation = building;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _buildingPlacementImage.sprite = _buildingInformation.BuildingPlacementImage;
        _nameOfBuilding.text = _buildingInformation.BuildingName;
    }

    private void PlaceBuilding(InputAction.CallbackContext callback)
    {
        if (this.gameObject.activeSelf == false) return;
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(_input.FindAction(_touchPositionKeyName).ReadValue<Vector2>());
        Instantiate(_buildingInformation.BuildingPrefab, touchPosition, Quaternion.identity);

        _onBuildingPlaced.Invoke();
    }
}
