using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndependentCoffeeTray : MonoBehaviour
{
     private int numberInTheListOfPlacesToPlaceObjects;
    private string nameOfTheObjectInTheArray;
    private void Start() 
    {
        numberInTheListOfPlacesToPlaceObjects = RayCasting.placesForTheTray.Count;
        RayCasting.placesForTheTray.Add(gameObject.transform);
        nameOfTheObjectInTheArray = RayCasting.placesForTheTray[numberInTheListOfPlacesToPlaceObjects].name;
        gameObject.SetActive(false);
    }
}
