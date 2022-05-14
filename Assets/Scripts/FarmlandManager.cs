using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmlandManager : MonoBehaviour
{
    public static FarmlandManager main;

    [SerializeField] Vector2 trimOffset;

    Vector2 minPoint;
    Vector2 maxPoint;

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
        minPoint = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        minPoint += trimOffset;
        maxPoint = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        maxPoint -= trimOffset;
    }

    public Vector3 GetRandomPoint()
    {
        return new Vector3(Random.Range(minPoint.x, maxPoint.x), Random.Range(minPoint.y, maxPoint.y), SPRITE_Z_VALUE);
    }
}
