using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private PlayerResourceManager.ResourceType _typeOfResource;
    [SerializeField] private float _transactionValue;
    public void CompleteTransaction()
    {
        Debug.Log("Completing transaction type: " + _typeOfTransaction + " " + _typeOfResource);
        switch (_typeOfTransaction)
        {
            case TypeOfTransaction.AddResource:
                PlayerResourceManager.Instance.AddToPlayerWallet(((int)_transactionValue), _typeOfResource);
                break;
            case TypeOfTransaction.SubResource:
                break;
            case TypeOfTransaction.AddMultiplier:
                PlayerResourceManager.Instance.AddToMultiplier(_transactionValue, _typeOfResource);
                break;
        }
    }
}
