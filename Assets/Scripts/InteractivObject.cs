using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivObject : MonoBehaviour
{
    private Rigidbody rb;
    private Collider cl;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        cl = GetComponent<Collider>();
    }

    public void RaisingAnObject() //поднятие объекта
    {
        rb.isKinematic = true;
        rb.useGravity = false;
        cl.isTrigger = true;
    }
    public void ObjectInstallation() // фиксация объекта
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        cl.isTrigger = false;
    }
    public void DropObject()
    {
        ObjectInstallation();
        rb.AddForce(transform.forward * 140);
    }
}
