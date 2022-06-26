using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dandelion : MonoBehaviour
{
    [Header("Customization")]
    [SerializeField] private float _lifeSpan = 60f;
    [SerializeField] private Gradient _colorShiftGradient;
    [SerializeField] private float _colorShiftFactor = 0.1f;
    private float currentColor = 0;
    private float colorChangeSpeed;

    [Header("Component References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Harvestable _harvestable;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _harvestable = GetComponent<Harvestable>();

        Invoke("Die", _lifeSpan);

        colorChangeSpeed = 1 / (_lifeSpan / _colorShiftFactor);
        InvokeRepeating("UpdateColor", 0f, _colorShiftFactor);
    }

    private void UpdateColor()
    {
        _spriteRenderer.color = _colorShiftGradient.Evaluate(currentColor);
        currentColor += colorChangeSpeed;
    }

    private void Die()
    {
        _harvestable.OnHarvestSuccessful.Invoke(_harvestable);
        Destroy(this.gameObject);
    }
}