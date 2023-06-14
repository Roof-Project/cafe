using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffemolca : MonoBehaviour
{
    public static InteractiveObject InteractiveObjectCoffeeBag;
    public static InteractiveObject InteractiveObjectTrray;
    private static Camera Camera;
    private static Transform CoffemolcaPrefab;
    private static GameObject Coffee;
    private static GameObject Player;

    private void Start() 
    {
        CoffemolcaPrefab = transform;
        InteractiveObjectCoffeeBag = transform.GetChild(3).GetComponent<InteractiveObject>();
        InteractiveObjectTrray = transform.GetChild(2).GetComponent<InteractiveObject>();
        Camera = transform.GetChild(5).GetComponent<Camera>();
        Coffee = transform.GetChild(6).gameObject;
    }
    public static void UsingACoffeeGrinder(GameObject _player)
    {
        if(InteractiveObjectCoffeeBag.interactiveObjectInPlace && InteractiveObjectTrray.interactiveObjectInPlace)
        {
            Player = _player;
            Camera.gameObject.SetActive(true);
            Player.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Coffee.SetActive(true);
            Destroy(CoffemolcaPrefab.GetChild(7).gameObject);
        }

    }
    public static void CheckingThePresenceOfTheCoffeeGrinderTray()
    {
        if(InteractiveObjectTrray.interactiveObjectInPlace == false) 
        {
            InteractiveObjectCoffeeBag.gameObject.SetActive(false);
        }
    }
    public static void TheCoffeeGrinderHasFinishedItsWork()
    {
        Camera.gameObject.SetActive(false);
        Player.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Coffee.SetActive(false);
    }
}
