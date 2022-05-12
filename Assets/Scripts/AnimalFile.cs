using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animal File", menuName = "ScriptableObjects/Create Animal File", order = 2)]
public class AnimalFile : ScriptableObject
{
    public string specie;
    public FoodFile preferedFood;
    [Tooltip("In meters")] public float maxHeight;
    [Tooltip("In meters")] public float minHeight;
    public float hungerFull;
    [Tooltip("Depletion per second")] public float hungerDepRate;
    [Tooltip("In years")] public int lifeSpan;
    public Sprite sprite;
    public Color tint;

}
