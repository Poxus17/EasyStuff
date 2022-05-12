using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Animal : MonoBehaviour
{
    [SerializeField] AnimalFile file;

    float hunger;
    KeyValuePair<int, int> height;

    int age;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    const float meterToInch = 39.3700787f; //conversion rate from meters to inches;

    public AnimalDataPacket myPacket {  get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        ApplyFile();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(file.specie);
        Debug.Log(GetHeight());
        Debug.Log(age);
        Debug.Log(GetProportionHunger());
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

    /// <summary>
    /// Get how full the hunger is
    /// </summary>
    /// <returns>A value between 0 and 1, 1 being full</returns>
    float GetProportionHunger()
    {
        return (hunger / file.hungerFull);
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
}
