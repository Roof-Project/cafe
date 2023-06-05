using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_serviceCoffeePackage : MonoBehaviour
{
    private int numberInTheListOfPlacesToPlaceObjects;
    private string nameOfTheObjectInTheArray;
    private void Start() 
    {
        numberInTheListOfPlacesToPlaceObjects = RayCasting.placeForACoffeeBag.Count;
        RayCasting.placeForACoffeeBag.Add(gameObject.transform);   
        nameOfTheObjectInTheArray = RayCasting.placeForACoffeeBag[numberInTheListOfPlacesToPlaceObjects].name;
        gameObject.SetActive(false);
    }
}
