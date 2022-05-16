using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Animal : MonoBehaviour
{
    public AnimalFile file;
    [SerializeField] GameObject FeedPrefab;
    [Space(10)]
    [Header("Shitty parameters")]
    [SerializeField] GameObject ShitPrefab;
    [SerializeField] Sprite shittingFace;
    [SerializeField] AudioClip shittingSound;

    float hunger;
    KeyValuePair<int, int> height;

    int age;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    const float METER_TO_INCH = 39.3700787f; //conversion rate from meters to inches;

    public static Animal observed;

    public AnimalDataPacket myPacket {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        GetComponent<Rigidbody2D>().gravityScale = 0;

        ApplyFile();

        InvokeRepeating("HungerTick", 1, 1.5f);
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
        if(hunger <= 0)
        {
            Destroy(gameObject);
        }

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
        float inches = height * METER_TO_INCH; //convert to inches

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
        if(hunger > file.hungerFull)
        {
            hunger = file.hungerFull;
        }

        GameObject jFeedInst = Instantiate(FeedPrefab, transform);
        SpriteRenderer jFeedRenderer = jFeedInst.GetComponent<SpriteRenderer>();

        if(jFeedRenderer != null)
        {
            jFeedRenderer.sprite = file.preferedFood.icon;
            float scale = spriteRenderer.sprite.bounds.size.x / jFeedRenderer.sprite.bounds.size.x;
            jFeedInst.transform.localScale = new Vector3(scale, scale, 1);
        }
        


        return GetProportionHunger();
    }

    #region State functions
    public void Talk(UnityAction whenFinished)
    {
        GetComponent<AudioSource>().clip = file.sound;
        GetComponent<AudioSource>().Play();
        whenFinished.Invoke();
    }

    /// <summary>
    /// I AM A FUCKING GENIUS
    /// </summary>
    /// <param name="whenFinished"></param>
    public void Shit(UnityAction whenFinished)
    {
        spriteRenderer.sprite = shittingFace;
        GetComponent<AudioSource>().clip = shittingSound;
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForShit(whenFinished));
    }

    IEnumerator WaitForShit(UnityAction whenFinished)
    {
        while (GetComponent<AudioSource>().isPlaying)
        {
            yield return null;
        }

        spriteRenderer.sprite = file.sprite;
        Instantiate(ShitPrefab, transform.position + (Vector3.left * spriteRenderer.sprite.bounds.size.x), Quaternion.identity);
        whenFinished.Invoke();
    }
    #endregion
}
