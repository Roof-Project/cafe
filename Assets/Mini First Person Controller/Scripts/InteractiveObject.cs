using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private Collider colliderObject;
    
    private Rigidbody rigidbodyObject;

    private GameObject openObject;
    private GameObject closedObject;

    private void Start() 
    {
        colliderObject = GetComponent<Collider>();
        rigidbodyObject = GetComponent<Rigidbody>();
        //closedObject = transform.GetChild(0).gameObject;
        //openObject = transform.GetChild(1).gameObject;    
    }

    public void TakingObject()
    {
        colliderObject.isTrigger = true;
        rigidbodyObject.isKinematic = true;
    }
    public void PutAnObject()
    {
        colliderObject.isTrigger = false;
        rigidbodyObject.isKinematic = false;
    }
    private void OnMouseEnter() 
    {
        Debug.Log("!");
    }
    private void OnMouseExit() 
    {
        Debug.Log("!!");
    }
}
