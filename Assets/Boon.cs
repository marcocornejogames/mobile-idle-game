using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boon : MonoBehaviour
{
    [Header("Component References")]


    [Header("Customization")]
    [SerializeField] private GameObject _boonToSpawn;
    [SerializeField] private float _boonSpawnDistance = 1f;
    [SerializeField] private float _boonRange = 5f;

    public void RegisterSpawn(Harvestable harvestable)
    {
        if (Vector2.Distance(harvestable.transform.position, this.transform.position) > _boonRange) return; //If spawned too far away, ignore;

        bool isValidPosition = false;
        Vector2 candidatePosition = Vector2.zero;
        while (!isValidPosition)
        {
            candidatePosition = MathTools.FindVector2WithinRange(harvestable.transform.position, _boonSpawnDistance);
            if (VillageBoundaries.Instance.IsInsideBounds(candidatePosition)) isValidPosition = true;
        }

        Instantiate(_boonToSpawn, candidatePosition, Quaternion.identity);
    }
}
