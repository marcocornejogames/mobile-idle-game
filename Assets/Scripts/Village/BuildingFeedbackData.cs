using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BuildingFeedbackData : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private BuildingInformation _buildingInfo;

    [Header("Customization")]
    [SerializeField] private float _buildingEffectRadius = 0f;


    public void SetRadius(float newRadius)
    {
        _buildingEffectRadius = newRadius;
    }

    public float GetRadius()
    {
        return _buildingEffectRadius;
    }

    public void SetBuildingInfo(BuildingInformation buildingInfo)
    {
        _buildingInfo = buildingInfo;
    }

}
