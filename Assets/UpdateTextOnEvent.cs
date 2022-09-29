using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateTextOnEvent : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateTextNumeral(float number)
    {
        _text.text = MathTools.ReadableNumber(number);
    }
}
