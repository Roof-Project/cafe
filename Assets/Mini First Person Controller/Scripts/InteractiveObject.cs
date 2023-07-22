using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private Collider colliderObject;//колайдер объекта
    private Rigidbody rigidbodyObject;//физика объекта

    private const float powerOfTheThrow = 250;//сила броска

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
    public void DropTheObject()
    {
        rigidbodyObject.isKinematic = false;
        rigidbodyObject.AddForce(transform.forward * powerOfTheThrow);
        colliderObject.isTrigger = false;
    }
}
