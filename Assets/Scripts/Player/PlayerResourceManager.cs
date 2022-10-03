using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    public static PlayerResourceManager Instance;
    public enum ResourceType
    {
        Food,
        Wood,
        Stone,
        Knowledge,
        Power
    }

    [Header("Component References")]
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private PlayerResourceUI _ResourceUI;

    [Header("Multipliers")]
    [SerializeField] private List<float> _foodIncomeMultiplier;
    [SerializeField] private List<float> _woodIncomeMultiplier;
    [SerializeField] private List<float> _stoneIncomeMultiplier;
    [SerializeField] private List<float> _knowledgeIncomeMultiplier;
    [SerializeField] private List<float> _powerIncomeMultiplier;


    //UNITY METHODS ________________________________________________________________________
    private void Awake()
    {
        //Manage Instances
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        _playerWallet = GetComponent<PlayerWallet>();

        //Initialize lists
        _foodIncomeMultiplier = new List<float>();
        _woodIncomeMultiplier = new List<float>();
        _stoneIncomeMultiplier = new List<float>();
        _knowledgeIncomeMultiplier = new List<float>();
        _powerIncomeMultiplier = new List<float>();
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
            case ResourceType.Food:
                _foodIncomeMultiplier.Add(amount);
                break;

            case ResourceType.Wood:
                _woodIncomeMultiplier.Add(amount);
                break;

            case ResourceType.Stone:
                _stoneIncomeMultiplier.Add(amount);
                break;

            case ResourceType.Knowledge:
                _knowledgeIncomeMultiplier.Add(amount);
                break;

            case ResourceType.Power:
                _powerIncomeMultiplier.Add(amount);
                break;
        }

    }
    public List<float> GetMultipliers(ResourceType typeOfResource)
    {
        List<float> multiplierList = new List<float>();

        switch (typeOfResource)
        {
            case ResourceType.Food:
                multiplierList = _foodIncomeMultiplier;
                break;

            case ResourceType.Wood:
                multiplierList = _woodIncomeMultiplier;
                break;

            case ResourceType.Stone:
                multiplierList = _stoneIncomeMultiplier;
                break;

            case ResourceType.Knowledge:
                multiplierList = _knowledgeIncomeMultiplier;
                break;

            case ResourceType.Power:
                multiplierList = _powerIncomeMultiplier;
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
