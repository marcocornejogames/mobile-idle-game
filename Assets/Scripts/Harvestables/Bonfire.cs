using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildingFeedbackData))]
public class Bonfire : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private BuildingFeedbackData _feedbackData;

    [Header("Customization")]
    [SerializeField] private float _speedBonus = 0.1f;
    [SerializeField] private bool _limitedTime = false;
    [SerializeField] private float _lifeSpan = 60f;
    [SerializeField] private float _range = 1f;


    private void Awake()
    {
        if(_limitedTime) Invoke("Die", _lifeSpan);

        _feedbackData = GetComponent<BuildingFeedbackData>();
        _feedbackData.SetRadius(_range);
    }

    public void RegisterSpawn(GoblinBrain goblin)
    {
        if (Vector2.Distance(this.transform.position, goblin.transform.position) > _range) return;
        UnitMovement goblinMovement = goblin.GetUnitMovement();
        goblinMovement.BoostSpeed(_speedBonus);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
