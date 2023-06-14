using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOfTheCoffeeBag : MonoBehaviour
{
    private GameObject coffeePackage;
   private void Start() 
   {
      coffeePackage = transform.GetChild(0).gameObject; 
      coffeePackage.SetActive(false);  
   }
   private void OnEnable() 
   {
        RayCasting.TakeABagOfCoffee += TheCupIsTaken;
        RayCasting.InstallACoffeePackage += TheCupIsPut;
   }
   private void TheCupIsTaken()
   {
      coffeePackage.SetActive(true);
   }
   private void TheCupIsPut()
   {
      coffeePackage.SetActive(false);
   }
}
