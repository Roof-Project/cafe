using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private Collider colliderObject;//колайдер объекта
    private Rigidbody rigidbodyObject;//физика объекта

    private void Start() 
    {
        colliderObject = GetComponent<Collider>();
        rigidbodyObject = GetComponent<Rigidbody>();
    }

    public void TakingObject()//взяли объект
    {
        colliderObject.isTrigger = true;
        rigidbodyObject.isKinematic = true;
    }
    public void PutAnObject()//положили объект
    {
        colliderObject.isTrigger = false;
        rigidbodyObject.isKinematic = false;
    }
    
}
