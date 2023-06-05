using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffemolca : MonoBehaviour
{
    private int numberInTheListOfPlacesToPlaceObjectsCoffeeBag;
    private int numberInTheListOfPlacesToPlaceObjectsTrray;
    private string nameOfTheObjectInTheArrayCoffeeBag;
    private string nameOfTheObjectInTheArrayTrray;
    public static InteractiveObject interactiveObjectCoffeeBag;
    public static InteractiveObject interactiveObjectTrray;
    private void Start() 
    {
        numberInTheListOfPlacesToPlaceObjectsCoffeeBag = RayCasting.placeForACoffeeBag.Count;
        numberInTheListOfPlacesToPlaceObjectsTrray = RayCasting.placesForTheTray.Count;
        RayCasting.placeForACoffeeBag.Add(transform.GetChild(3));
        RayCasting.placesForTheTray.Add(transform.GetChild(2));
        interactiveObjectCoffeeBag = transform.GetChild(3).GetComponent<InteractiveObject>();
        interactiveObjectTrray = transform.GetChild(2).GetComponent<InteractiveObject>();
    }
    
    public static void CheckingThePresenceOfTheCoffeeGrinderTray()
    {
        if(interactiveObjectTrray.interactiveObjectInPlace == false) 
        {
            interactiveObjectCoffeeBag.gameObject.SetActive(false);
        }
    }
}
