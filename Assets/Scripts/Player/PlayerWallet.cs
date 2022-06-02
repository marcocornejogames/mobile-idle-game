using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] private float _magica = 0;
    [SerializeField] private float _alchemy = 0;
    [SerializeField] private float _occult = 0;
    [SerializeField] private float _money = 0;
    [SerializeField] private float _prestige = 0;

    //CUSTOM METHODS _________________________________________
    public void AddToWallet(float amount, PlayerResourceManager.ResourceType typeOfResource)
    {
        switch (typeOfResource)
        {
            case PlayerResourceManager.ResourceType.Magica:
                _magica += amount;
                break;

            case PlayerResourceManager.ResourceType.Alchemy:
                _alchemy += amount;
                break;

            case PlayerResourceManager.ResourceType.Occult:
                _occult += amount;
                break;

            case PlayerResourceManager.ResourceType.Money:
                _money += amount;
                break;

            case PlayerResourceManager.ResourceType.Prestige:
                _prestige += amount;
                break;
        }
    }


    public void RemoveFromWallet(float amount, PlayerResourceManager.ResourceType typeOfResource)
    {
        switch (typeOfResource)
        {
            case PlayerResourceManager.ResourceType.Magica:
                _magica -= amount;
                break;

            case PlayerResourceManager.ResourceType.Alchemy:
                _alchemy -= amount;
                break;

            case PlayerResourceManager.ResourceType.Occult:
                _occult -= amount;
                break;

            case PlayerResourceManager.ResourceType.Money:
                _money -= amount;
                break;

            case PlayerResourceManager.ResourceType.Prestige:
                _prestige -= amount;
                break;
        }
    }
    public float GetValue(PlayerResourceManager.ResourceType typeOfResource)
    {
        float returnValue = 0;

        switch (typeOfResource)
        {
            case PlayerResourceManager.ResourceType.Magica:
                returnValue = _magica;
                break;

            case PlayerResourceManager.ResourceType.Alchemy:
                returnValue = _alchemy;
                break;

            case PlayerResourceManager.ResourceType.Occult:
                returnValue = _occult;
                break;

            case PlayerResourceManager.ResourceType.Money:
                returnValue = _money;
                break;

            case PlayerResourceManager.ResourceType.Prestige:
                returnValue = _prestige;
                break;
        }

        return returnValue;
    }
}
