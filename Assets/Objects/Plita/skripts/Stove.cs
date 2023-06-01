using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    private static GameObject stoveOn;
    private static GameObject stoveOff;
    private static bool _activ;
    void Start()
    {
        stoveOn = gameObject.transform.GetChild(1).GetChild(0).gameObject;
        stoveOff = gameObject.transform.GetChild(1).GetChild(1).gameObject;
        RayCasting.placeForTurks.Add(transform.GetChild(3));
    }
    public static void TurningOnAndOffTheStove()
    {
        _activ = !_activ;
        if(_activ)
        {
            stoveOff.SetActive(false);
            stoveOn.SetActive(true);
            return;
        }
        else if(!_activ)
        {
            stoveOff.SetActive(true);
            stoveOn.SetActive(false);
            return;
        }
    }
}
