using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatingHut : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private GameObject _goblinPrefab;
    [SerializeField] private InputActionAsset _inputAction;
    [SerializeField] private string _onClickActionName = "Click";

    [Header("Customization")]
    [SerializeField] private int _gobsPerSec = 1;
    [SerializeField] private int _gobsPerTap = 1;

    private void Awake()
    {
        Invoke("Cooldown", 1);

        _inputAction.FindAction(_onClickActionName).performed += OnClick;
    }

    private void Spawn()
    {
        for (int i = 0; i < _gobsPerSec; i++)
        {
            Instantiate(_goblinPrefab, this.transform.position, Quaternion.identity);
        }

        Invoke("Cooldown", 1);
    }

    private void Cooldown()
    {
        Spawn();
    }

    private void OnClick(InputAction.CallbackContext callback)
    {
        for (int i = 0; i < _gobsPerTap; i++)
        {
            Instantiate(_goblinPrefab, this.transform.position, Quaternion.identity);
        }
    }
}
