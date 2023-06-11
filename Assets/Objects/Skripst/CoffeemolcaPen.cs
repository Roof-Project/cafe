using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class help : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private static Vector3 MousePosition;
    private static Transform Grip; 
    private static Animator CoffeeAnimator;
    private static Rigidbody Rb;
    private static bool CheckingWhetherTheHandleIsTaken;
    private static bool TheHandleMoves;
    private static bool TheCoffeeGrinderIsWorking;
    private static int TheTimeThatTheCoffeeGrinderBegs = 60;
    private int theTimeThatTheCoffeeGrinderIsPrayingForNow;
    private void Start() 
    {
        Grip = transform;
        Rb = GetComponent<Rigidbody>();
        CoffeeAnimator = transform.parent.GetChild(6).GetComponent<Animator>();
        CoffeeAnimator.speed = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CheckingWhetherTheHandleIsTaken = false;
        CoffeeAnimator.speed = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CheckingWhetherTheHandleIsTaken = true;
        TheCoffeeGrinderIsWorking = true;
        StartCoroutine(Twist());
        StartCoroutine(CoffeeGrinderTimer());
    }
    
    private IEnumerator Twist()
    {
        while(true)
        {
            if(TheCoffeeGrinderIsWorking)
            {
                if(CheckingWhetherTheHandleIsTaken)
                {
                    MousePosition = new Vector2(Screen.width / 2  - Input.mousePosition.x , Screen.height / 2 - Input.mousePosition.y) ; 
                    Vector3 difference = MousePosition -Grip.position;
                    difference.Normalize();
                    float rotation_z = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
                    Grip.rotation = Quaternion.Euler(-90, 0f, rotation_z+90);
                    if(Rb.IsSleeping())
                    {
                        if(TheHandleMoves)
                        {
                            TheHandleMoves = false;
                            CoffeeAnimator.speed = 0;
                        }    
                    }
                    else
                    {
                        if(TheHandleMoves == false)
                        {
                            TheHandleMoves = true;
                            CoffeeAnimator.speed = 1;
                        }
                    }
                }
                yield return null; 
            }
            else
            {
                yield break;
            }
        }
    }
    private IEnumerator CoffeeGrinderTimer()
    {
        while(true)
        {
            if((theTimeThatTheCoffeeGrinderIsPrayingForNow <= TheTimeThatTheCoffeeGrinderBegs) && (TheHandleMoves))
            {
                theTimeThatTheCoffeeGrinderIsPrayingForNow ++;
                Debug.Log(theTimeThatTheCoffeeGrinderIsPrayingForNow);
            }
            if(theTimeThatTheCoffeeGrinderIsPrayingForNow >= TheTimeThatTheCoffeeGrinderBegs)
            {
                Coffemolca.TheCoffeeGrinderHasFinishedItsWork();
                TheCoffeeGrinderIsWorking = false;
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
    }
}

