using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    public GameObject er;
    public byte interactiveObjectLayer;
    void Start()
    {
        
    }
    void Update()
    {
        CheckingObjectTags();
    }
    private void FixedUpdate()
    {
        
    }
    private void CheckingObjectTags()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if(Physics.Raycast(ray, out _hit, 3f, interactiveObjectLayer))
        {

            switch(_hit.collider.gameObject.tag)
            {
                case "test":
                    er.transform.position = _hit.point+new Vector3(0,0.3f,0);
                break;
            }
        }
    }
}
