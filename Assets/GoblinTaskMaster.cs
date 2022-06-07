using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinTaskMaster : MonoBehaviour
{
    public static GoblinTaskMaster Instance;

    [Header("Task List")]
    [SerializeField] private List<Harvestable> _toDoList;
    [SerializeField] private List<Harvestable> _inProgressList;

    private void Awake()
    {
        //Manage Instances
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        _toDoList = new List<Harvestable>();
        _inProgressList = new List<Harvestable>();
    }


    //______________________________________________________Harvestable Task Interactions
    public void AddToTaskList(Harvestable harvestable)
    {
        _toDoList.Add(harvestable);
    }

    public void SetTaskDone(Harvestable harvestable)
    {
        _inProgressList.Remove(harvestable);
    }

    // ____________________________________________________Goblin Task Interaction

    public bool IsTaskAvailable()
    {
        return _toDoList.Count > 0;
    }

    public void ClaimTask(Harvestable harvestable)
    {
        _toDoList.Remove(harvestable);
        _inProgressList.Add(harvestable);
    }

    public void GiveUp(Harvestable harvestable)
    {
        _inProgressList.Remove(harvestable);
        _toDoList.Add(harvestable);
    }

    public List<Harvestable> GetTaskList()
    {
        if (_toDoList.Count > 0) return _toDoList;
        else return null;
    }
}
