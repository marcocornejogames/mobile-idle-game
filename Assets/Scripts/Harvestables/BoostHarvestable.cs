using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostHarvestable : MonoBehaviour
{
    [Header("Customization")]
    [SerializeField] private float _boostRange = 5f;
    [SerializeField] private int _boostMultiplier = 2;

    public void RegisterSpawn(Harvestable harvestable)
    {
        if (Vector2.Distance(harvestable.transform.position, this.transform.position) > _boostRange) return; //If spawned too far away, ignore;

        harvestable.ResourceBonus(_boostMultiplier);

    }
}
