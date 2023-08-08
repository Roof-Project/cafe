using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

internal class RayCaster : MonoBehaviour
{
    private Camera _camera; //главная камера игрока
    
    private bool objectInHand = false; //объект в руке
    private bool theObjectIsBeingInstalled = false; //объект устанавливается 


    [SerializeField] private Transform armObject;

    private Transform raisedObject;
    private Vector3 rotationRaisedObject;

    [Header("слои объектов")]                       
    [SerializeField] private LayerMask[] layerMask; /* 0- small object
                                                       1- defalt
   
                                                     */

    void Start()
    {
        Initialization();
    }
    void Update()
    {
        TakeAnObject();
        //InstallAnObject();
        InstallAnObjectFix();
        DropTheObject();
    }

    private void TakeAnObject()//поднимаем маленький объект 
    {
        if(
            Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 4, layerMask[0]) && 
            Input.GetKeyDown(ControlSystem.tace) && 
            objectInHand == false
        )
        {
            Debug.Log(hit.collider.name);
            raisedObject = hit.transform;
            raisedObject.transform.position = armObject.transform.position;
            raisedObject.transform.parent = armObject.transform;
            raisedObject.GetComponent<InteractivObject>().RaisingAnObject();
            raisedObject.rotation = armObject.rotation;
            objectInHand = true;
        }
    }

    private void InstallAnObject()//установка объекта зажатая кнопка 
    {
        if (objectInHand == false) return;

        if(
            Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 4, layerMask[1]) &&
            Input.GetKey(ControlSystem.install)  
        )
        {
            raisedObject.position = hit.point;
            raisedObject.rotation = Quaternion.Euler(rotationRaisedObject);
        }
        if(Input.GetKeyUp(ControlSystem.Install))
        {
            Fixed();
        }

        if(Input.GetMouseButton(0))
        {
            rotationRaisedObject.y += 100 * Time.deltaTime;
        }
        if (Input.GetMouseButton(1))
        {
            rotationRaisedObject.y -= 100 * Time.deltaTime;
        }
    }

    private void InstallAnObjectFix()//установка объекта  
    {
        if (objectInHand == false) return;

        if (Input.GetKeyDown(ControlSystem.install) && theObjectIsBeingInstalled == false)
        {
            theObjectIsBeingInstalled = !theObjectIsBeingInstalled;
            return;
        }

        if (
            Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 4, layerMask[1]) &&
            theObjectIsBeingInstalled == true
        )
        {
            raisedObject.position = hit.point;
            raisedObject.rotation = Quaternion.Euler(rotationRaisedObject);
            if (Input.GetKeyDown(ControlSystem.Install) && theObjectIsBeingInstalled == true)
            {
                raisedObject.position = hit.point;
                Fixed();
                theObjectIsBeingInstalled = !theObjectIsBeingInstalled;
                return;
            }
        }
        
        if (Input.GetMouseButton(0))
        {
            rotationRaisedObject.y += 100 * Time.deltaTime;
        }
        if (Input.GetMouseButton(1))
        {
            rotationRaisedObject.y -= 100 * Time.deltaTime;
        }
    }

    private void DropTheObject() //бросить объект
    {
        if (
          Input.GetKeyDown(ControlSystem.drop) &&
          theObjectIsBeingInstalled == false &&
          objectInHand == true
        )
        {
            raisedObject.GetComponent<InteractivObject>().DropObject();
            Fixed();
        }
    }

    private void Initialization() //инициализация переменных
    {
        _camera = Camera.main;
        armObject = _camera.transform.GetChild(0);
    }
    private void Fixed() //фиксаия объекта
    {
        //raisedObject.position = hit.point;
        raisedObject.parent = null;
        raisedObject.GetComponent<InteractivObject>().ObjectInstallation();
        raisedObject = null;
        objectInHand = false;
    }
}
