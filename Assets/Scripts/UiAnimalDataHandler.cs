using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAnimalDataHandler : MonoBehaviour
{
    public static UiAnimalDataHandler main;

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

}
