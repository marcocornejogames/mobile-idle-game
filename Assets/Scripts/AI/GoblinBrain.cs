using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBrain : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private UnitMovement _unitMovement;


    [Header("Customization")]
    [SerializeField] private float _goblinLifespan = 25f;

    [Header("Idle")]
    [SerializeField] private float _idleMovementRange = 2f;
    [SerializeField] private float _idleWaitingTimeMIN = 3f;
    [SerializeField] private float _idleWaitingTimeMAX = 5f;
    [Range(0, 1)] [SerializeField] private float _chanceOfIdleWaiting = 0.5f;

    [Header("Tasking")]
    [SerializeField] private Harvestable _currentTask;


    [Header("Feedback")]
    [SerializeField] private GoblinState _currentState = GoblinState.Idle;

    [Header("Events")]
    [SerializeField] private GoblinScriptableEvent _onGoblinSpawnEvent;
    [SerializeField] private GoblinScriptableEvent _onGoblinDeathEvent;

    private bool _isWaiting;


    //Enums
    private enum GoblinState
    {
        Idle,
        Tasking,
    }

    // UNIT METHODS ______________________________________________________
    private void Awake()
    {
        _unitMovement = GetComponent<UnitMovement>();

        _onGoblinSpawnEvent.Invoke(this);
        //Debug.Log("Goblin lifespan = " + _goblinLifespan);
        Invoke("Die", _goblinLifespan);


    }

    private void Update()
    {
        switch (_currentState)
        {
            case GoblinState.Idle:
                Idle();
                break;
            case GoblinState.Tasking:
                Task();
                break;
            default:
                break;
        }
    }

    // CUSTOM METHODS __________________________________________________________________

    //__________________________________________________________________________STATE MACHINE
    
    // IDLE ______________________________________________________________________________
    private void Idle()
    {
        if((_isWaiting || _unitMovement.GetHasTarget()) && GoblinTaskMaster.Instance.IsTaskAvailable()) //Is idly waiting or idly moving about
        {
            StopWaiting();
            _unitMovement.Stop();
            _currentState = GoblinState.Tasking;
            return;
            
        }

        if(!_isWaiting && Random.value <= _chanceOfIdleWaiting) //Chance to just wait for a bit
        {
            _isWaiting = true;
            Invoke("StopWaiting", Random.Range(_idleWaitingTimeMIN, _idleWaitingTimeMAX));
        }

        if(!_unitMovement.GetHasTarget() && !_isWaiting) //Is idling but not moving around right now
        {
            Vector2 newTargetPosition = MathTools.FindVector2WithinRange(transform.position, _idleMovementRange);
            if (VillageBoundaries.Instance.IsInsideBounds(newTargetPosition)) _unitMovement.GoTo(newTargetPosition);
            else _unitMovement.Stop();
        }
    }

    private void StopWaiting()
    {
        _isWaiting = false;
    }

    // TASKING ________________________________________________________________________________
    private void Task()
    {
        if(_currentTask == null) // Has no task
        {
            if(GoblinTaskMaster.Instance.IsTaskAvailable()) // Tasks are available
            {
                _currentTask = FindClosestTask(GoblinTaskMaster.Instance.GetTaskList());
                GoblinTaskMaster.Instance.ClaimTask(_currentTask);
            }
            else // No available task
            {
                _currentState = GoblinState.Idle;
                return;
            }
        }

        // HAS TASK

        if (Vector2.Distance(this.transform.position, _currentTask.transform.position) > _currentTask.HarvestRange) // NOT CLOSE ENOUGH TO TASK
        {
            _unitMovement.GoTo(_currentTask.transform.position);
            return;
        }
        else _unitMovement.Stop();

        if (!_currentTask.HarvestInProgress)
        {
            _currentTask.TryHarvest(); // If not already harvesting, start
            _currentTask.OnHarvestSuccessful.AddListener(ClearTask);
        }
    }

    private void ClearTask(Harvestable harvestable)
    {
        if(harvestable == _currentTask) _currentTask = null;
        _currentState = GoblinState.Idle;
    }

    private Harvestable FindClosestTask(List<Harvestable> listOfTasks)
    {
        Harvestable closestTask = listOfTasks[0];
        foreach (Harvestable task in listOfTasks)
        {
            if (Vector2.Distance(this.transform.position, task.transform.position) < Vector2.Distance(this.transform.position, closestTask.transform.position)) closestTask = task;
        }
        return closestTask;
    }

    // DEATH _________________________________________________________

    private void Die()
    {
        _onGoblinDeathEvent.Invoke(this);

        if (_currentTask != null)
        {
            GoblinTaskMaster.Instance.GiveUp(_currentTask);
            if (_currentTask.HarvestInProgress)
            {
                _currentTask.CancelInvoke();
            }
            ClearTask(_currentTask);
        }

        Destroy(this.gameObject);
    }

    //ACCESS _____________________________________________________________
    public UnitMovement GetUnitMovement()
    {
        return _unitMovement;
    }

    public float GetLifespan()
    {
        return _goblinLifespan;
    }

    public void SetUnitLifespan(float newLifespan)
    {
        _goblinLifespan = newLifespan;
    }

 
}
