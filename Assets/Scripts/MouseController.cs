using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    bool readingAnimal;
    bool onShit;
    GameObject pieceOfShit;

    UiAnimalDataHandler animalDataHandler;
    FarmManager farm;

    // Start is called before the first frame update
    void Start()
    {
        readingAnimal = false;
        animalDataHandler = UiAnimalDataHandler.main;
        farm = FarmManager.main;
    }

    public void Hover(InputAction.CallbackContext context)
    {
        onShit = false;
        RaycastHit2D hit = RaycastMouse(context.ReadValue<Vector2>());
        AnimalDataPacket transferPacket;
        if (hit.collider != null)
        {
            Animal detectedAnimal = hit.collider.gameObject.GetComponent<Animal>();
            Animal.observed = detectedAnimal;

            readingAnimal = (detectedAnimal != null);

            if (readingAnimal)
            {
                transferPacket = detectedAnimal.myPacket;
                animalDataHandler.AttachDataToAnimal(detectedAnimal.transform.position);
                animalDataHandler.UpdateHunger(detectedAnimal.GetProportionHunger());
            }
            else
            {
                transferPacket = null;
                if (hit.collider.gameObject.tag == "SHIT")
                {
                    onShit = true;
                    pieceOfShit = hit.collider.gameObject;
                }
            }
            
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
        if (context.started)
        {
            if (readingAnimal)
            {
                if (Animal.observed.GetPrefferedFood() == farm.GetCurrentResource() && farm.ConfirmFood())
                {
                    animalDataHandler.UpdateHunger(Animal.observed.Feed());
                    farm.addFood(-1);
                }
            }
            else if(onShit)
            {
                Destroy(pieceOfShit);
            }
        }
        
        
    }

    RaycastHit2D RaycastMouse(Vector2 screenPos)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenPos);

        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        return Physics2D.Raycast(mousePos2D, Vector2.zero); ;
    }
}
