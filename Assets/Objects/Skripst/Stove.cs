using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    private static Animator stoveAnimation;
    private static bool _activ;
    void Start()
    {
        stoveAnimation = gameObject.transform.GetChild(1).GetChild(0).GetComponent<Animator>();
        RayCasting.placeForTurks.Add(transform.GetChild(3));
    }
    public static void TurningOnAndOffTheStove()
    {
        _activ = !_activ;
        if(_activ)
        {
            stoveAnimation.Play("plitaOn");
            return;
        }
        else if(!_activ)
        {
            stoveAnimation.Play("plitaOff");
            return;
        }
    }
}
