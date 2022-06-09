using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
    [Header("Customization")]
    [SerializeField] private PlayerResourceManager.ResourceType _typeOfResourceBonus;
    [SerializeField] private int _bonusAmount = 1;
    [SerializeField] private float _spawnBonusRange = 10f;

    public void RegisterSpawn(Harvestable harvestable)
    {
        if (Vector2.Distance(harvestable.transform.position, this.transform.position) > _spawnBonusRange) return; //If spawned too far away, ignore;

        PlayerResourceManager.Instance.AddToPlayerWallet(_bonusAmount, _typeOfResourceBonus);
    }
}
