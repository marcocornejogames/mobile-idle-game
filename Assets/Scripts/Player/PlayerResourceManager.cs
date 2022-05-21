using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    public enum ResourceType
    {
        Magica,
        Alchemy,
        Occult,
        Money,
        Prestige
    }

    [Header("Component References")]
    [SerializeField] private PlayerWallet _playerWallet;

    [Header("Multipliers")]
    [SerializeField] private List<float> _magicaIncomeMultiplier;
    [SerializeField] private List<float> _alchemyIncomeMultiplier;
    [SerializeField] private List<float> _occultIncomeMultiplier;
    [SerializeField] private List<float> _moneyIncomeMultiplier;
    [SerializeField] private List<float> _prestigeIncomeMultiplier;


    //UNITY METHODS ________________________________________________________________________
    private void Awake()
    {
        _playerWallet = GetComponent<PlayerWallet>();

        _magicaIncomeMultiplier = new List<float>();
        _alchemyIncomeMultiplier = new List<float>();
        _occultIncomeMultiplier = new List<float>();
        _moneyIncomeMultiplier = new List<float>();
        _prestigeIncomeMultiplier = new List<float>();
    }

    //CUSTOM METHODS _______________________________________________________________________
    public void AddToPlayerWallet(int amount, ResourceType typeOfResource)
    {
        _playerWallet.AddToWallet(ApplyMultipliers(amount, GetMultipliers(typeOfResource)), typeOfResource);
    }

    public List<float> GetMultipliers(ResourceType typeOfResource)
    {
        List<float> multiplierList = new List<float>();

        switch (typeOfResource)
        {
            case ResourceType.Magica:
                multiplierList = _magicaIncomeMultiplier;
                break;

            case ResourceType.Alchemy:
                multiplierList = _alchemyIncomeMultiplier;
                break;

            case ResourceType.Occult:
                multiplierList = _occultIncomeMultiplier;
                break;

            case ResourceType.Money:
                multiplierList = _moneyIncomeMultiplier;
                break;

            case ResourceType.Prestige:
                multiplierList = _prestigeIncomeMultiplier;
                break;

            default:
                break;
        }

        return multiplierList;
    }

    private int ApplyMultipliers(int startingAmount, List<float> listOfMultipliers)
    {

        float currentAmount = startingAmount;
        foreach (float multiplier in listOfMultipliers)
        {
            currentAmount *= multiplier;
        }

        return (int)currentAmount;
    }


}
