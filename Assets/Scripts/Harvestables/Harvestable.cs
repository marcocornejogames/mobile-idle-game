using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Harvestable : MonoBehaviour
{
    [Header("Customization")]
    [SerializeField] private PlayerResourceManager.ResourceType _resourceType;
    [SerializeField] private int _resourceAmount;
    [SerializeField] private bool _taskOnAwake = true;
    [SerializeField] private float _harvestTime = 5f;

    public float HarvestRange = 0.5f;
    public bool HarvestInProgress = false;
    public UnityEvent<Harvestable> OnHarvestableSpawn;
    public UnityEvent<Harvestable> OnHarvestSuccessful;

    private void Awake()
    {
        OnHarvestableSpawn.Invoke(this);
    }
    private void Start()
    {
       if (_taskOnAwake) Task();
    }

    private void Task()
    {
        GoblinTaskMaster.Instance.AddToTaskList(this);
    }

    public void TryHarvest()
    {
        //Debug.Log("Harvesting Started");
        HarvestInProgress = true;
        Invoke("Harvest", _harvestTime);
    }
    private void Harvest()
    {
        HarvestInProgress = false;
        PlayerResourceManager.Instance.AddToPlayerWallet(_resourceAmount, _resourceType);
        GoblinTaskMaster.Instance.SetTaskDone(this);
        OnHarvestSuccessful.Invoke(this);
        Destroy(this.gameObject);
    }


    public void ResourceBonus(int bonusMultiplier)
    {
        _resourceAmount *= bonusMultiplier;
    }

}
