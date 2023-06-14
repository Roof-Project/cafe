using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupIsTaken : MonoBehaviour
{
   private GameObject cup;
   private void Start() 
   {
      cup = transform.GetChild(0).gameObject;   
   }
   private void OnEnable() 
   {
        RayCasting.TakingACup += TheCupIsTaken;
        RayCasting.PutTheCupDown += TheCupIsPut;
   }
   private void TheCupIsTaken()
   {
      cup.SetActive(true);
   }
   private void TheCupIsPut()
   {
      cup.SetActive(false);
   }
}
