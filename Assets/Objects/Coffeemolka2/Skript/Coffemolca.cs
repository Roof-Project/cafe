using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffemolca : MonoBehaviour
{
    static private Material matCoffemolca;
    static private GameObject placeForTheTray;
    static private MeshRenderer meshObject;
    private void Start() 
    {
        matCoffemolca = GetComponent<Material>();
        placeForTheTray = transform.GetChild(2).gameObject;    
        meshObject = GetComponent<MeshRenderer>();
        placeForTheTray.SetActive(false);
        //RayCasting.placesForTheTray.Add(placeForTheTray);
    }
}
