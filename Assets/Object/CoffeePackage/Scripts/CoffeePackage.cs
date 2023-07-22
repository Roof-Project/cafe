using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeePackage : MonoBehaviour
{
    
    [SerializeField] private GameObject aPackedBagOfCoffee;//закрытый пакет кофе
    [SerializeField] private GameObject unpackedCoffeeBag;//открытый пакет кофе

    public bool packagingCondition = true; //состояние упаковки


    public void UnpackThePackage()//распоковали пакет
    {
        aPackedBagOfCoffee.SetActive(false);
        unpackedCoffeeBag.SetActive(true);
        packagingCondition = false;
        gameObject.tag = "unpacked coffee bag";
    }
    public void PackAPackage()//запоковали назад
    {
        aPackedBagOfCoffee.SetActive(true);
        unpackedCoffeeBag.SetActive(false);
        packagingCondition = true;
        gameObject.tag = "a packed bag of coffee";
    }
}
