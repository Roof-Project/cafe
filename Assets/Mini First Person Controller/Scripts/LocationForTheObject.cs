using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationForTheObject : MonoBehaviour
{
    private string nameObject;
    private GameObject childObject;
    public bool IsTheSpaceBeingUsed;//используется ли место

    private void Start() 
    {
        nameObject = transform.tag;
        childObject = transform.GetChild(0).gameObject;
    }

    private void OnEnable() 
    {
        EventBus.OnShowPlaces += ShowAPlaceToInstall;
        EventBus.OnHideAPlace += HideTheInstallationLocation;
    }
    private void OnDisable() 
    {
        EventBus.OnShowPlaces -= ShowAPlaceToInstall;
        EventBus.OnHideAPlace -= HideTheInstallationLocation;
    }

    private void ShowAPlaceToInstall(string tagObject)//паказать место для установки
    {
        if(nameObject != tagObject) return;
        childObject.SetActive(true);
    }
    private void HideTheInstallationLocation(string tagObject)//спрятать место для установки
    {
        if(nameObject != tagObject) return;
        childObject.SetActive(false);
    }
}
