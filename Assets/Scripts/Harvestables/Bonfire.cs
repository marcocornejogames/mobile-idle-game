using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    [Header("Customization")]
    [SerializeField] private float _speedBonus = 0.1f;
    [SerializeField] private bool _limitedTime = false;
    [SerializeField] private float _lifeSpan = 60f;
    [SerializeField] private float _range = 1f;


    private void Awake()
    {
        if(_limitedTime) Invoke("Die", _lifeSpan);
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
