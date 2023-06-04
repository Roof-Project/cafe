using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffemolca : MonoBehaviour
{
    private void Start() 
    {
        RayCasting.placeForACoffeeBag.Add(transform.GetChild(3));
        RayCasting.placesForTheTray.Add(transform.GetChild(2));
    }
}
