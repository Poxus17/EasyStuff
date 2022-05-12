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
    [SerializeField] Vector2 spawnAreaTrim;
    Vector2 minSpawnPoint;
    Vector2 maxSpawnPoint;

    int currentIndex;

    const float SPRITE_Z_VALUE = 0;

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

        minSpawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        minSpawnPoint += spawnAreaTrim;
        maxSpawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        maxSpawnPoint -= spawnAreaTrim;
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
        GameObject cub = Instantiate(emptyAnimalPrefab, RollPosition(), Quaternion.identity);
        cub.GetComponent<Animal>().file = files[currentIndex];
    }

    void UpdateDisplay()
    {
        animalName.text = files[currentIndex].specie;
    }

    Vector3 RollPosition()
    {
        return new Vector3(Random.Range(minSpawnPoint.x, maxSpawnPoint.x), Random.Range(minSpawnPoint.y, maxSpawnPoint.y), SPRITE_Z_VALUE);
    }
}
