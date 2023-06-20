using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOption : MonoBehaviour
{
    [Header("Управление")]
    public static KeyCode UseObject;

    //интерфейс
    private static GameObject DefaultCursor;
    private static GameObject InteractiveCursor;
    //рука
    public static Transform ArmTransform;
    public static Transform ObjectInHand;

    public static bool objectInHand;
    
    //слой для луча

    //делегаты
    public delegate void coffeePackage();
    public static event coffeePackage TakeABagOfCoffee;
    public static event coffeePackage APlaceForCoffee;

    public LayerMask[] layerMasks;
    public int layerMasksId;
    private void Start() 
    {
        UseObject = KeyCode.Mouse0;

        DefaultCursor = transform.GetChild(4).GetChild(0).gameObject;
        InteractiveCursor = transform.GetChild(4).GetChild(1).gameObject;

        ArmTransform = transform.GetChild(0).GetChild(0);
    }
    private void Update() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, layerMasks[layerMasksId]))
        {
            if(objectInHand == false)
            {
                switch (hit.transform.tag)
                {
                    case "coffee package":
                        UsingTheObject(true);
                        if(Input.GetKeyDown(UseObject))
                        {
                            hit.collider.GetComponent<CoffeePackage>().Taking();
                            TakeABagOfCoffee?.Invoke();
                            objectInHand = true;
                            return;
                        }
                    break;
                }
            }
            if(objectInHand == true)
            {
                switch (hit.transform.tag)
                {
                    case "place of the coffee package":
                        UsingTheObject(true);
                        if(Input.GetKeyDown(UseObject))
                        {
                            APlaceForCoffee?.Invoke();
                            hit.collider.GetComponent<PlaceForACoffeeBag>().Hiding();
                            objectInHand = false;
                            
                            return;
                        }
                    break;
                }
            }
        }
        else
        {
            UsingTheObject(false);
        }
    }
    public static void UsingTheObject(bool _activ)
    {
        if(_activ)
        {
            DefaultCursor.SetActive(false);
            InteractiveCursor.SetActive(true);
        }
        if(_activ == false)
        {
            DefaultCursor.SetActive(true);
            InteractiveCursor.SetActive(false);
        }
    }

    
}
