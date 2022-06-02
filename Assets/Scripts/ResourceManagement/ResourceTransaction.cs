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
    [SerializeField] private PlayerResourceManager.ResourceType[] _typesOfResource;
    [SerializeField] private float[] _transactionValues;

    [Header("Feedback")]
    [SerializeField] private UnityEvent _transactionFailedEvent;
    [SerializeField] private UnityEvent _transactionSuccessEvent;

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
}
