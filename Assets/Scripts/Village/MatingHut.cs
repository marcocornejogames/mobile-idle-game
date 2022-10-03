using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class MatingHut : MonoBehaviour, IInformational
{
    [Header("Component References")]
    [SerializeField] private BuildingInformation _buildingInfo;
    [SerializeField] private GameObject _goblinPrefab;
    [SerializeField] private InputActionAsset _inputAction;
    [SerializeField] private string _onClickActionName = "Click";

    [Header("Customization")]
    [SerializeField] private int _gobsPerSec = 1;
    [SerializeField] private int _gobsPerTap = 1;

    [Header("Feedback")]
    [SerializeField] private int _totalGoblinsBirthed = 0;

    [Header("Events")]
    [SerializeField] private UnityEvent _onGoblinSpawn;

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
            _totalGoblinsBirthed++;

            _onGoblinSpawn.Invoke();
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
            _totalGoblinsBirthed++;

            _onGoblinSpawn.Invoke();
        }
    }

    //DISPLAY FEEDBACK______________________________________________________
    public void PopulateInformation(BuildingFeedbackDisplay feedbackDisplay)
    {
        _buildingInfo.BasicInformationDisplay(feedbackDisplay);

        string feedbackText =
            "Total goblins birthed: " + _totalGoblinsBirthed;

        feedbackDisplay.SetBody(feedbackText);
    }

    public GameObject GetObject()
    {
        return this.gameObject;
    }
}
