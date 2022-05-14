using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class AnimalStateMachine : MonoBehaviour
{
    public StateOddsPair[] stateValues;
}

public enum State { idle, walk, talk, shit};

[System.Serializable]
public class StateOddsPair 
{
    [SerializeField] State state;
    [SerializeField][Range(0.0f,1.0f)] float odds;

    public KeyValuePair<State,float> GetPair()
    {
        return new KeyValuePair<State, float>(state, odds);
    }
}

