using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinNameGenerator : MonoBehaviour
{
    public static GoblinNameGenerator Instance;

    [Header("Customization")]
    [SerializeField] private int _minimumNumberOfSyllables;
    [SerializeField] private int _maxNumberOfSyllables;

    [Header("Name Database")]
    [SerializeField] private string[] _syllables;
    [SerializeField] private string[] _conjunctions;



    private void Awake()
    {
        //Manage Instances
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }


    public string GetRandomName()
    {
        string goblinName = "";
        int numberOfSyllables = Random.Range(_minimumNumberOfSyllables, _maxNumberOfSyllables);
        for (int i = 0; i < numberOfSyllables; i++)
        {
            if (goblinName != "") goblinName = goblinName + _conjunctions[Random.Range(0, _conjunctions.Length)];
            goblinName = goblinName + _syllables[Random.Range(0, _syllables.Length)];
        }

        return goblinName;
    }
}
