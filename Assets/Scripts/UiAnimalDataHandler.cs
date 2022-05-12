using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiAnimalDataHandler : MonoBehaviour
{
    public static UiAnimalDataHandler main;

    [SerializeField] TextMeshProUGUI specie;
    [SerializeField] TextMeshProUGUI age;
    [SerializeField] TextMeshProUGUI height;
    [SerializeField] Image hunger;
    [SerializeField] GameObject childPanel;

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
    }

    public void UpdateUiData(AnimalDataPacket? packet)
    {
        if(packet == null)
        {
            childPanel.SetActive(false);
        }
        else
        {
            childPanel.SetActive(true);
            specie.text = packet.specie;
            age.text = packet.age;
            height.text = packet.height;
        }
        
    }

    public void UpdateHunger(float fill)
    {
        hunger.fillAmount = fill;
    }

    public bool GetVisible()
    {
        return childPanel.activeSelf;
    }
}

public class AnimalDataPacket 
{
    public string specie { get; private set; }
    public string age { get; private set; }
    public string height { get; private set; }

    public AnimalDataPacket(string specie, string age, string height)
    {
        this.specie = specie;
        this.age = age;
        this.height = height;
    }
}

