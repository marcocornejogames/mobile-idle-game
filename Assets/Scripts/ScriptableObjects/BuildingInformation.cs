using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName =("Scriptable Object/Building Information"))]
public class BuildingInformation : ScriptableObject
{
    [Header("Purchase")]
    public string BuildingName;
    public Sprite BuildingShopImage;
    public string BuildingDescription;
    public int foodCost;
    public int woodCost;
    public int stoneCost;
    public int knowledgeCost;
    public int powerCost;

    [Header("Placement")]
    public Sprite BuildingPlacementImage;
    public GameObject BuildingPrefab;


    public int GetCost(PlayerResourceManager.ResourceType resourceType)
    {
        int toBeReturned = 0;
        switch (resourceType)
        {
            case PlayerResourceManager.ResourceType.Food:
                toBeReturned = foodCost;
                break;

            case PlayerResourceManager.ResourceType.Wood:
                toBeReturned = woodCost;
                break;

            case PlayerResourceManager.ResourceType.Stone:
                toBeReturned = stoneCost;
                break;

            case PlayerResourceManager.ResourceType.Knowledge:
                toBeReturned = knowledgeCost;
                break;

            case PlayerResourceManager.ResourceType.Power:
                toBeReturned = powerCost;
                break;

            default:
                break;
        }

        return toBeReturned;
    }

    public void ApplyCostMultiplier(float multiplier)
    {
        foodCost *= (int)multiplier;
        woodCost *= (int)multiplier;
        stoneCost *= (int)multiplier;
        knowledgeCost *= (int)multiplier;
        powerCost *= (int)multiplier;
    }
}
