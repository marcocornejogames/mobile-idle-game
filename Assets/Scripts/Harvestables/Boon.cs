using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boon : MonoBehaviour
{
    [Header("Customization")]
    [SerializeField] private GameObject _boonToSpawn;
    [SerializeField] private float _boonSpawnDistance = 1f;
    [SerializeField] private float _boonRange = 5f;
    [SerializeField][Range(0,1)] private float _boonChance = 1f;

    public void RegisterSpawn(Harvestable harvestable)
    {
        if (Vector2.Distance(harvestable.transform.position, this.transform.position) > _boonRange) return; //If spawned too far away, ignore;
        if (Random.Range(0f,1f) > _boonChance) return;


        bool isValidPosition = false;
        Vector2 candidatePosition = Vector2.zero;
        while (!isValidPosition)
        {
            candidatePosition = MathTools.FindVector2WithinRange(harvestable.transform.position, _boonSpawnDistance);
            if (VillageBoundaries.Instance.IsInsideBounds(candidatePosition)) isValidPosition = true;
        }

        Instantiate(_boonToSpawn, candidatePosition, Quaternion.identity);
    }

    public void RegisterGoblin(GoblinBrain goblin)
    {
        if (Vector2.Distance(goblin.transform.position, this.transform.position) > _boonRange) return; //If spawned too far away, ignore;
        if (Random.Range(0f, 1f) > _boonChance) return;

        bool isValidPosition = false;
        Vector2 candidatePosition = Vector2.zero;
        while (!isValidPosition)
        {
            candidatePosition = MathTools.FindVector2WithinRange(goblin.transform.position, _boonSpawnDistance);
            if (VillageBoundaries.Instance.IsInsideBounds(candidatePosition)) isValidPosition = true;
        }

        Instantiate(_boonToSpawn, candidatePosition, Quaternion.identity);
    }
}
