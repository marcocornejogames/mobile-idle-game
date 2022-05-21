using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] private int _magica = 0;
    [SerializeField] private int _alchemy = 0;
    [SerializeField] private int _occult = 0;
    [SerializeField] private int _money = 0;
    [SerializeField] private int _prestige = 0;

    //CUSTOM METHODS _________________________________________
    public void AddToWallet(int amount, PlayerResourceManager.ResourceType typeOfResource)
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
}
