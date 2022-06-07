using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerResourceUI : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private TextMeshProUGUI _totalFood;
    [SerializeField] private TextMeshProUGUI _totalWood;
    [SerializeField] private TextMeshProUGUI _totalStone;
    [SerializeField] private TextMeshProUGUI _totalKnowledge;
    [SerializeField] private TextMeshProUGUI _totalPower;


    public void UpdateResourceTotal(float amount, PlayerResourceManager.ResourceType typeOfResource)
    {

        TextMeshProUGUI textToUpdate = _totalWood;
        switch (typeOfResource)
        {
            case PlayerResourceManager.ResourceType.Food:
                textToUpdate = _totalFood;
                break;

            case PlayerResourceManager.ResourceType.Wood:
                textToUpdate = _totalWood;
                break;

            case PlayerResourceManager.ResourceType.Stone:
                textToUpdate = _totalStone;
                break;

            case PlayerResourceManager.ResourceType.Knowledge:
                textToUpdate = _totalKnowledge;
                break;

            case PlayerResourceManager.ResourceType.Power:
                textToUpdate = _totalPower;
                break;
        }

        textToUpdate.text = MathTools.ReadableNumber(amount);
    }

}
