using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food File", menuName = "ScriptableObjects/Create Food file", order = 1)]
public class FoodFile : ScriptableObject
{
    public string name;
    public float nutrition;
    public Sprite icon;
    public Color tint;
}
