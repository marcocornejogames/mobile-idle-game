using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceTransaction : MonoBehaviour
{
    private enum TypeOfTransaction
    {
        AddResource,
        SubResource,
        AddMultiplier
    }
    [Header("Customization")]
    [SerializeField] private TypeOfTransaction _typeOfTransaction;

    [Header("Feedback")]
    [SerializeField] private BuildingInformation _buildingInfo;
    [SerializeField] private PlayerResourceManager.ResourceType[] _typesOfResource;
    [SerializeField] private float[] _transactionValues;

    [Header("Events")]
    [SerializeField] private UnityEvent _transactionFailedEvent;
    [SerializeField] private UnityEvent _transactionSuccessEvent;



    private void Awake()
    {
        _typesOfResource = new PlayerResourceManager.ResourceType[5]
            {   PlayerResourceManager.ResourceType.Food,
                PlayerResourceManager.ResourceType.Wood,
                PlayerResourceManager.ResourceType.Stone,
                PlayerResourceManager.ResourceType.Knowledge,
                PlayerResourceManager.ResourceType.Power};

        _transactionValues = new float[5]{ 0, 0, 0, 0, 0};
    }
    private void Update()
    {
        if(_typeOfTransaction == TypeOfTransaction.SubResource && _buildingInfo != null) UpdateTransacitonValues();
    }

    private void UpdateTransacitonValues()
    {
        int index = 0;
        foreach (PlayerResourceManager.ResourceType resourceType in _typesOfResource)
        {
            switch (resourceType)
            {
                case PlayerResourceManager.ResourceType.Food:
                    _transactionValues[index] = _buildingInfo.foodCost;
                    break;

                case PlayerResourceManager.ResourceType.Wood:
                    _transactionValues[index] = _buildingInfo.woodCost;
                    break;

                case PlayerResourceManager.ResourceType.Stone:
                    _transactionValues[index] = _buildingInfo.stoneCost;
                    break;

                case PlayerResourceManager.ResourceType.Knowledge:
                    _transactionValues[index] = _buildingInfo.knowledgeCost;
                    break;

                case PlayerResourceManager.ResourceType.Power:
                    _transactionValues[index] = _buildingInfo.powerCost;
                    break;

            }

            index++;
        }
    }

    public void CompleteTransaction()
    {

        //Check balance if subtraction
        if (_typeOfTransaction == TypeOfTransaction.SubResource)
        {
            if (!ValidateWithdrawal()) //Cancel Transaction if not enough funds
            {
                _transactionFailedEvent.Invoke();
                Debug.Log("ERROR: NOT ENOUGH FUNDS");
                return;
            }

        }

        int index = 0;
        foreach (PlayerResourceManager.ResourceType typeOfResource in _typesOfResource)
        {
            switch (_typeOfTransaction)
            {
                case TypeOfTransaction.AddResource:
                    PlayerResourceManager.Instance.AddToPlayerWallet(((int)_transactionValues[index]), typeOfResource);
                    break;

                case TypeOfTransaction.SubResource:
                    PlayerResourceManager.Instance.RemoveFromWallet((int)_transactionValues[index], typeOfResource);
                    break;

                case TypeOfTransaction.AddMultiplier:
                    PlayerResourceManager.Instance.AddToMultiplier(_transactionValues[index], typeOfResource);
                    break;
            }

            index++;
        }

        _transactionSuccessEvent.Invoke();
    }

    private bool ValidateWithdrawal()
    {
        if (_typeOfTransaction != TypeOfTransaction.SubResource) return true; //If not trying to withdraw, dont need to validate

        bool isValid = true;
        int index = 0;
        foreach (PlayerResourceManager.ResourceType typeOfResource in _typesOfResource)
        {
            if(_transactionValues[index] > PlayerResourceManager.Instance.CheckPlayerBalance(typeOfResource))
            {
                isValid = false;
                continue;
            }

            index++;
        }
        return isValid;
    }

    public void SetBuildingInfo(BuildingInformation info)
    {
        _buildingInfo = info;
    }
}
