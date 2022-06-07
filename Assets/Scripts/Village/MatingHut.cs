using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatingHut : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private GameObject _goblinPrefab;

    [Header("Customization")]
    [SerializeField] private int _gobsPerSec = 1;

    private void Awake()
    {
        Invoke("Cooldown", 1);
    }

    private void Spawn()
    {
        for (int i = 0; i < _gobsPerSec; i++)
        {
            Instantiate(_goblinPrefab, this.transform.position, Quaternion.identity);
        }

        Invoke("Cooldown", 1);
    }

    private void Cooldown()
    {
        Spawn();
    }
}
