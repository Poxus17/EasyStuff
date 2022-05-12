using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    bool readingAnimal;
    UiAnimalDataHandler animalDataHandler;

    // Start is called before the first frame update
    void Start()
    {
        readingAnimal = false;
        animalDataHandler = UiAnimalDataHandler.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hover(InputAction.CallbackContext context)
    {
        Vector2 screenPos = context.ReadValue<Vector2>();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenPos);

        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        AnimalDataPacket transferPacket;
        if (hit.collider != null)
        {
            Animal detectedAnimal = hit.collider.gameObject.GetComponent<Animal>();

            readingAnimal = (detectedAnimal != null);

            transferPacket = detectedAnimal.myPacket;

            animalDataHandler.AttachDataToAnimal(detectedAnimal.transform.position);
        }
        else
        {
            readingAnimal = false;
            transferPacket = null;
        }

        if (animalDataHandler.GetVisible() != readingAnimal)
        {
            animalDataHandler.UpdateUiData(transferPacket);
        }
    }

    public void Click(InputAction.CallbackContext context)
    {
        Debug.Log(context.started);
    }

}
