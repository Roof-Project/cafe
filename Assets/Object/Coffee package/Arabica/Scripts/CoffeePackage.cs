using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEditor.EventSystems;

public class CoffeePackage : MonoBehaviour
{
    private Rigidbody rb;
    private Transform transformObject;
    private Collider colliderObject;
    

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
        transformObject = transform;
        colliderObject = GetComponent<Collider>();  
    }
    public void Taking()
    {
        if(transformObject.parent)
            transformObject.parent.GetComponent<PlaceForACoffeeBag>().isUsed = false;

        PlayerOption.objectInHand = true;
        rb.isKinematic = true;
        transformObject.position = PlayerOption.ArmTransform.position;
        transformObject.parent = PlayerOption.ArmTransform;
        colliderObject.enabled = false;
        transformObject.rotation = Quaternion.Euler(0,0,0);
        PlayerOption.ObjectInHand = transform;
    }
}
