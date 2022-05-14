using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AnimalStateMachine : MonoBehaviour
{
    [SerializeField] float minTimer;
    [SerializeField] float maxTimer;
    [SerializeField] float speed;
    [Space(10)]
    [Header("States")]
    [Tooltip("Be advised, states will be rolled in array order")] public StateOddsAction[] stateValues;

    public State currentState { get; private set; }

    private void Start()
    {
        RunMachine();
    }

    void RunMachine()
    {
        bool runningState = false;
        foreach(StateOddsAction soa in stateValues) //Sword online art
        {
            if(soa.odds >= Random.Range(0f, 1f))
            {
                runningState = true;
                currentState = soa.state;
                soa.action.Invoke(TimerDelay);
                break;
            }
        }

        if (!runningState)
            TimerDelay();
            
    }

    void TimerDelay()
    {
        currentState = State.idle;
        Invoke("RunMachine", Random.Range(minTimer, maxTimer));
    }

    public void Walk(UnityAction whenFinished)
    {
        StartCoroutine(WalkRoutine(whenFinished));
    }

    IEnumerator WalkRoutine(UnityAction whenFinished)
    {
        Vector3 target = FarmlandManager.main.GetRandomPoint();

        while(Vector3.Distance(transform.position,target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;

        whenFinished.Invoke();
    }

    public void Shit(UnityAction whenFinished)
    {
        Debug.Log("Shit");
        whenFinished.Invoke();
    }
}

public enum State { idle ,walk, talk, shit};

[System.Serializable]
public class StateOddsAction
{
    public State state;
    [Range(0.0f,1.0f)] public float odds;
    public MachineEvent action;
}

[System.Serializable]
public class MachineEvent: UnityEvent<UnityAction> { }
