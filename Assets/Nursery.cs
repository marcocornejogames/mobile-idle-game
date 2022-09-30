using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildingFeedbackData))]
public class Nursery : MonoBehaviour
{
    [Header("Customization")]
    [SerializeField] private float _lifespanBonus = 0.5f;
    [SerializeField] private float _range = 5f;

    [Header("Component References")]
    [SerializeField] private BuildingFeedbackData _feedbackData;
    public void RegisterSpawn(GoblinBrain goblin)
    {
        if (Vector2.Distance(this.transform.position, goblin.transform.position) > _range) return;
        float currentLifespan = goblin.GetLifespan();
        goblin.SetUnitLifespan(currentLifespan *= (1 +_lifespanBonus));
    }

    private void Awake()
    {
        _feedbackData = GetComponent<BuildingFeedbackData>();
        _feedbackData.SetRadius(_range);
    }
}
