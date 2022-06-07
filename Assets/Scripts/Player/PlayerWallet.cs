using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] private float _food = 0;
    [SerializeField] private float _wood = 0;
    [SerializeField] private float _stone = 0;
    [SerializeField] private float _knowledge = 0;
    [SerializeField] private float _power = 0;

    //CUSTOM METHODS _________________________________________
    public void AddToWallet(float amount, PlayerResourceManager.ResourceType typeOfResource)
    {
        switch (typeOfResource)
        {
            case PlayerResourceManager.ResourceType.Food:
                _food += amount;
                break;

            case PlayerResourceManager.ResourceType.Wood:
                _wood += amount;
                break;

            case PlayerResourceManager.ResourceType.Stone:
                _stone += amount;
                break;

            case PlayerResourceManager.ResourceType.Knowledge:
                _knowledge += amount;
                break;

            case PlayerResourceManager.ResourceType.Power:
                _power += amount;
                break;
        }
    }


    public void RemoveFromWallet(float amount, PlayerResourceManager.ResourceType typeOfResource)
    {
        switch (typeOfResource)
        {
            case PlayerResourceManager.ResourceType.Food:
                _food -= amount;
                break;

            case PlayerResourceManager.ResourceType.Wood:
                _wood -= amount;
                break;

            case PlayerResourceManager.ResourceType.Stone:
                _stone -= amount;
                break;

            case PlayerResourceManager.ResourceType.Knowledge:
                _knowledge -= amount;
                break;

            case PlayerResourceManager.ResourceType.Power:
                _power -= amount;
                break;
        }
    }
    public float GetValue(PlayerResourceManager.ResourceType typeOfResource)
    {
        float returnValue = 0;

        switch (typeOfResource)
        {
            case PlayerResourceManager.ResourceType.Food:
                returnValue = _food;
                break;

            case PlayerResourceManager.ResourceType.Wood:
                returnValue = _wood;
                break;

            case PlayerResourceManager.ResourceType.Stone:
                returnValue = _stone;
                break;

            case PlayerResourceManager.ResourceType.Knowledge:
                returnValue = _knowledge;
                break;

            case PlayerResourceManager.ResourceType.Power:
                returnValue = _power;
                break;
        }

        return returnValue;
    }
}
