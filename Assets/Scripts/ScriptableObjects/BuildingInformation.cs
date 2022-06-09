using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName =("Scriptable Object/Building Information"))]
public class BuildingInformation : ScriptableObject
{
    public string BuildingName;
    public Sprite BuildingShopImage;
    public Sprite BuildingPlacementImage;
    public GameObject BuildingPrefab;
}
