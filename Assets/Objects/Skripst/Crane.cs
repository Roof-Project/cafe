using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour
{
    private static Animator animatedPen;
    private static Transform craneObj;
    private int numberInTheListOfPlacesToPlaceObjects;
    private string nameOfTheObjectInTheArray;
    public static InteractiveObject interactiveObject;
    private static bool _activ;
    private static Crane crane;

    private void Start() 
    {
        numberInTheListOfPlacesToPlaceObjects = RayCasting.placeForTurks.Count;
        RayCasting.placeForTurks.Add(transform.GetChild(4));
        nameOfTheObjectInTheArray = RayCasting.placeForTurks[numberInTheListOfPlacesToPlaceObjects].name;
        interactiveObject = transform.GetChild(4).GetComponent<InteractiveObject>();
        animatedPen = transform.GetChild(2).GetComponent<Animator>();
        craneObj = transform;
        crane = this;
    }

    public static void UseTheFaucet()
    {
        _activ = !_activ;
        if(_activ)
        {
            animatedPen.Play("cranOn");
            crane.StartCoroutine(Filling());
            return;
        }
        else if(!_activ)
        {
            animatedPen.Play("cranOff");
            crane.StopCoroutine(Filling());
            return;
        }
    }
    private static IEnumerator Filling()
    {
        while(true)
        {
            if(interactiveObject.interactiveObjectInPlace && _activ)
            {
                Debug.Log("q");
                yield return null;
            }
            yield return null;
        }
    }
}
