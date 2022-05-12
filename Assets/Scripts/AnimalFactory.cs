using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimalFactory : MonoBehaviour
{
    public static AnimalFactory main;

    [SerializeField] AnimalFile[] files;
    [SerializeField] TextMeshProUGUI animalName;
    [SerializeField] GameObject emptyAnimalPrefab;

    int currentIndex;

    private void Awake()
    {
        if (main != default)
        {
            Destroy(this);
        }
        else
        {
            main = this;
        }
    }

    private void Start()
    {
        currentIndex = 0;
    }

    public void menuButtonPress(int dir)
    {
        dir = (dir / Mathf.Abs(dir)); // It doenst matter what is the value only if it's positive or negative
        currentIndex += dir;

        if (currentIndex < 0)
        {
            currentIndex = 0;
        }
        else if (currentIndex >= files.Length)
        {
            currentIndex = files.Length - 1;
        }

        UpdateDisplay();
    }

    public void MakeAnimal()
    {
        GameObject cub = Instantiate(emptyAnimalPrefab);
        cub.GetComponent<Animal>().file = files[currentIndex];
    }

    void UpdateDisplay()
    {
        animalName.text = files[currentIndex].specie;
    }
}
