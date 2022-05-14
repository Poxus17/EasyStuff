using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimalStateMachine))]
public class AnimalStateMachineEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AnimalStateMachine sm = (AnimalStateMachine)target;

        string[] enumNames = System.Enum.GetNames(typeof(State));

        if (sm.stateValues.Length < enumNames.Length)
        {
            EditorGUILayout.HelpBox("Not all States have odds. \nStates with no odds will not occure", MessageType.Warning);
        }
        else
        {
            foreach(string s in enumNames)
            {
                bool found = false;
                foreach (StateOddsAction p in sm.stateValues)
                {
                    if(s == p.state.ToString())
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    EditorGUILayout.HelpBox("Not all States have odds. \nStates with no odds will not occure", MessageType.Warning);
                }
            }
        }
    }
}
