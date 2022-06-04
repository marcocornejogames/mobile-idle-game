using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementCurve : MonoBehaviour
{
    [Header("Customization")]
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _incrementFactor;
    [SerializeField] private float _minimumAmount;
    [SerializeField] private float _maximumAmount;

    [Header("Feedback")]
    [SerializeField] private float _currentIncrement;
    [SerializeField] private float _valueAtCurrentIncrement;

    private void Awake()
    {
        _currentIncrement = _incrementFactor;
    }

    private void Update()
    {
        _valueAtCurrentIncrement = ValueAtCurrentIncrement();
    }

    public void Increment(int numberOfIncrements)
    {
        for (int i = 0; i < numberOfIncrements; i++)
        {
            _currentIncrement += _incrementFactor; 
        }

        if (_currentIncrement > 1) _currentIncrement = 1;
        Debug.Log($"Incremented {numberOfIncrements} times! Current increment value: {_currentIncrement}.");
    }

    public float ValueAtCurrentIncrement()
    {
        float curveValue = _curve.Evaluate(_currentIncrement);
        return MathTools.Remap(curveValue, 0, 1, _minimumAmount, _maximumAmount);
    }
}
