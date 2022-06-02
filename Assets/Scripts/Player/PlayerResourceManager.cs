using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class PlayerResourceManager : MonoBehaviour
{
    public static PlayerResourceManager Instance;
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
    [SerializeField] private PlayerResourceUI _ResourceUI;

    [Header("Multipliers")]
    [SerializeField] private List<float> _magicaIncomeMultiplier;
    [SerializeField] private List<float> _alchemyIncomeMultiplier;
    [SerializeField] private List<float> _occultIncomeMultiplier;
    [SerializeField] private List<float> _moneyIncomeMultiplier;
    [SerializeField] private List<float> _prestigeIncomeMultiplier;


    //UNITY METHODS ________________________________________________________________________
    private void Awake()
    {
        //Manage Instances
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        _playerWallet = GetComponent<PlayerWallet>();

        //Initialize lists
        _magicaIncomeMultiplier = new List<float>();
        _alchemyIncomeMultiplier = new List<float>();
        _occultIncomeMultiplier = new List<float>();
        _moneyIncomeMultiplier = new List<float>();
        _prestigeIncomeMultiplier = new List<float>();
    }

    //CUSTOM METHODS _______________________________________________________________________

    //_______________________________________________________________________ PLAYER WALLET
    public void AddToPlayerWallet(float amount, ResourceType typeOfResource)
    {
        _playerWallet.AddToWallet(ApplyMultipliers(amount, GetMultipliers(typeOfResource)), typeOfResource);
        _ResourceUI.UpdateResourceTotal(_playerWallet.GetValue(typeOfResource), typeOfResource);
    }

    public void RemoveFromWallet(float amount, ResourceType typeOfResource)
    {
        _playerWallet.RemoveFromWallet(amount, typeOfResource);
        _ResourceUI.UpdateResourceTotal(_playerWallet.GetValue(typeOfResource), typeOfResource);
    }
    public float CheckPlayerBalance(ResourceType typeOfResource)
    {
        return _playerWallet.GetValue(typeOfResource);
    }

    //_________________________________________________________________________MULTIPLIERS
    public void AddToMultiplier(float amount, ResourceType typeOfResource)
    {

        Debug.Log("Adding to multiplier:" + typeOfResource);
        switch (typeOfResource)
        {
            case ResourceType.Magica:
                _magicaIncomeMultiplier.Add(amount);
                break;

            case ResourceType.Alchemy:
                _alchemyIncomeMultiplier.Add(amount);
                break;

            case ResourceType.Occult:
                _occultIncomeMultiplier.Add(amount);
                break;

            case ResourceType.Money:
                _moneyIncomeMultiplier.Add(amount);
                break;

            case ResourceType.Prestige:
                _prestigeIncomeMultiplier.Add(amount);
                break;
        }

        _ResourceUI.UpdateMultiplierTotal(ApplyMultipliers(1, GetMultipliers(typeOfResource)), typeOfResource);
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

    private float ApplyMultipliers(float startingAmount, List<float> listOfMultipliers)
    {

        float currentAmount = startingAmount;
        foreach (float multiplier in listOfMultipliers)
        {
            currentAmount *= multiplier;
        }

        return currentAmount;
    }

}
