using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerResourceUI : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private TextMeshProUGUI _totalMagicaText;
    [SerializeField] private TextMeshProUGUI _incrementalMagicaText;
    [SerializeField] private TextMeshProUGUI _totalAlchemyText;
    [SerializeField] private TextMeshProUGUI _incrementalAlchemyText;
    [SerializeField] private TextMeshProUGUI _totalOccultText;
    [SerializeField] private TextMeshProUGUI _incrementalOccultText;
    [SerializeField] private TextMeshProUGUI _totalMoneyText;
    [SerializeField] private TextMeshProUGUI _incrementalMoneyText;
    [SerializeField] private TextMeshProUGUI _totalPrestigeText;
    [SerializeField] private TextMeshProUGUI _incrementalPrestigeText;


    public void UpdateResourceTotal(int amount, PlayerResourceManager.ResourceType typeOfResource)
    {
        TextMeshProUGUI textToUpdate = _totalAlchemyText;
        switch (typeOfResource)
        {
            case PlayerResourceManager.ResourceType.Magica:
                textToUpdate = _totalMagicaText;
                break;

            case PlayerResourceManager.ResourceType.Alchemy:
                textToUpdate = _totalAlchemyText;
                break;

            case PlayerResourceManager.ResourceType.Occult:
                textToUpdate = _totalOccultText;
                break;

            case PlayerResourceManager.ResourceType.Money:
                textToUpdate = _totalMoneyText;
                break;

            case PlayerResourceManager.ResourceType.Prestige:
                textToUpdate = _totalPrestigeText;
                break;
        }

        textToUpdate.text = amount.ToString();
    }

    public void UpdateMultiplierTotal (int amount, PlayerResourceManager.ResourceType typeOfResource)
    {
        TextMeshProUGUI textToUpdate = _incrementalAlchemyText;
        switch (typeOfResource)
        {
            case PlayerResourceManager.ResourceType.Magica:
                textToUpdate = _incrementalMagicaText;
                break;

            case PlayerResourceManager.ResourceType.Alchemy:
                textToUpdate = _incrementalAlchemyText;
                break;

            case PlayerResourceManager.ResourceType.Occult:
                textToUpdate = _incrementalOccultText;
                break;

            case PlayerResourceManager.ResourceType.Money:
                textToUpdate = _incrementalMoneyText;
                break;

            case PlayerResourceManager.ResourceType.Prestige:
                textToUpdate = _incrementalPrestigeText;
                break;
        }

        textToUpdate.text = amount.ToString();
    }
}
