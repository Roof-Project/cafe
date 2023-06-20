using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceForACoffeeBag : MonoBehaviour
{
    private bool activeItems;
    private bool waitForUsage;
    public bool isUsed;
    

    private GameObject placeOfTheCoffeeBag;

    private Transform objectInHand;

    private void Start() 
    {
        PlayerOption.APlaceForCoffee += DeactivatingItems;
        PlayerOption.TakeABagOfCoffee += ActiveItems;

        placeOfTheCoffeeBag = transform.GetChild(0).gameObject; 
    }
    private void ActiveItems()
    {
        if(isUsed == false)
        {
            activeItems = true;
            placeOfTheCoffeeBag.SetActive(true);
        }
    }
    private void DeactivatingItems()
    {
        activeItems = false;
        placeOfTheCoffeeBag.SetActive(false);

    }
    public void Hiding()
    {
        objectInHand = PlayerOption.ObjectInHand;
        objectInHand.GetComponent<Rigidbody>().isKinematic = false;
        objectInHand.GetComponent<Collider>().enabled = true;
        objectInHand.parent = transform;
        objectInHand.position = transform.position;
        objectInHand.rotation = transform.rotation;
        isUsed = true;
        PlayerOption.objectInHand = false;
    }
}
