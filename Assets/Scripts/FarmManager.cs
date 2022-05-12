using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FarmManager : MonoBehaviour
{
    [SerializeField] FoodFile[] farmResources;
    [SerializeField] TextMeshProUGUI foodName;
    [SerializeField] TextMeshProUGUI foodAmount;

    int[] resourceValues;

    int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        /* ResourceValues is defined by the content of farmResources
         * they both read values by currentIndex, and all start from 0
         * so resourceValues don't care who's value they're keeping
         * they just know it's their index twin */
        resourceValues = new int[farmResources.Length]; 
        for(int i = 0; i < resourceValues.Length; i++)
        {
            resourceValues[i] = 0;
        }

        currentIndex = 0;

        updateResourceUi();
    }

    public void menuButtonPress(int dir)
    {
        dir = (dir / Mathf.Abs(dir)); // It doenst matter what is the value only if it's positive or negative
        currentIndex += dir;

        if(currentIndex < 0)
        {
            currentIndex = 0;
        }
        else if(currentIndex >= farmResources.Length)
        {
            currentIndex = farmResources.Length - 1;
        }
        Debug.Log(currentIndex);

        updateResourceUi();
    }

    void updateResourceUi()
    {
        foodName.text = farmResources[currentIndex].name;
        foodAmount.text = resourceValues[currentIndex].ToString();
    }

    public void addFood(int amount)
    {
        resourceValues[currentIndex] += amount;
        updateResourceUi();
    }

}

