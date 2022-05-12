using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Animal : MonoBehaviour
{
    public AnimalFile file;

    float hunger;
    KeyValuePair<int, int> height;

    int age;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    const float meterToInch = 39.3700787f; //conversion rate from meters to inches;

    public static Animal observed;

    public AnimalDataPacket myPacket {  get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        GetComponent<Rigidbody2D>().gravityScale = 0;

        
    }

    // Start is called before the first frame update
    void Start()
    {
        ApplyFile();
        Debug.Log(file.specie);
        Debug.Log(GetHeight());
        Debug.Log(age);
        Debug.Log(GetProportionHunger());

        InvokeRepeating("HungerTick", 1, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ApplyFile()
    {
        hunger = file.hungerFull;
        height = ConvertHeightToFeet();
        age = (int)Random.Range(0, file.lifeSpan);

        spriteRenderer.sprite = file.sprite;
        spriteRenderer.color = file.tint;

        Vector2 S = spriteRenderer.sprite.bounds.size;
        boxCollider.size = S;
        //boxCollider.offset = new Vector2((S.x / 2), 0);


        myPacket = new AnimalDataPacket(file.specie, age.ToString(), GetHeight());
    }

    void HungerTick()
    {
        hunger -= file.hungerDepRate;

        if(this == observed)
        {
            UiAnimalDataHandler.main.UpdateHunger(GetProportionHunger());
        }
    }

    /// <summary>
    /// Get how full the hunger is
    /// </summary>
    /// <returns>A value between 0 and 1, 1 being full</returns>
    public float GetProportionHunger()
    {
        return hunger / file.hungerFull;
    }

    KeyValuePair<int, int> ConvertHeightToFeet()
    {
        float height = Random.Range(file.minHeight, file.maxHeight); //roll height
        float inches = height * meterToInch; //convert to inches

        return new KeyValuePair<int, int>((int)inches / 12, (int)inches % 12); // return as pair
    }

    string GetHeight()
    {
        return (height.Key + "\'" + height.Value + "\"");
    }

    public FoodFile GetPrefferedFood()
    {
        return file.preferedFood;
    }

    public float Feed()
    {
        hunger += file.preferedFood.nutrition;
        return GetProportionHunger();
    }
}
