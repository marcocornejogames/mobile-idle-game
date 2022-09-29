using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableSpawner : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private GameObject _harvestablePrefab;

    [Header("Customization")]
    [SerializeField] private int _harvestablesPerSec = 2;
    [SerializeField] private float _radiusRange = 20f;
    [SerializeField] private int _harvestableLimit = 50;
    [SerializeField] private bool _populateOnStart = true;

    private List<GameObject> _allActiveHarvestables;

    private void Awake()
    {
        _allActiveHarvestables = new List<GameObject>();

        Invoke("Cooldown", 1);
    }

    private void Start()
    {
        if (_populateOnStart) Populate();
    }
    private void Update()
    {
        _allActiveHarvestables.RemoveAll(GameObject => GameObject == null);
    }
    private void Spawn()
    {
        for (int i = 0; i < _harvestablesPerSec; i++)
        {
            if (_allActiveHarvestables.Count >= _harvestableLimit) continue;
            GameObject newObject = Instantiate(_harvestablePrefab, GetRandomPosition(), Quaternion.identity);
            _allActiveHarvestables.Add(newObject);
        }

        Invoke("Cooldown", 1);
    }

    private void Cooldown()
    {
        Spawn();
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 candidatePosition = this.transform.position;
        bool isValidPosition = false;
        while(!isValidPosition)
        {
            candidatePosition = MathTools.FindVector2WithinRange(this.transform.position, _radiusRange);
            isValidPosition = VillageBoundaries.Instance.IsInsideBounds(candidatePosition);
        }
        return candidatePosition;
    }

    private void Populate()
    {
        for (int i = 0; i < (_harvestableLimit- _harvestablesPerSec); i++)
        {
            if (_allActiveHarvestables.Count >= _harvestableLimit) return;
            GameObject newObject = Instantiate(_harvestablePrefab, GetRandomPosition(), Quaternion.identity);
            _allActiveHarvestables.Add(newObject);
        }
    }
}
