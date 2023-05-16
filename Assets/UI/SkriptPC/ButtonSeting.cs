using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSeting : MonoBehaviour, IPointerClickHandler
{
    public static bool isOpen;
    [SerializeField] int idProgramm;

    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("peredYsloviem");
        if (eventData.clickCount == 2 && isOpen == false)
        {
            isOpen = true;
            eventData.clickCount = 0;
            Debug.Log(eventData.clickCount);
            WindowOFF.windowOFF.DefouldSetingWindow(idProgramm);
            WindowOFF.windowOFF.UnderPanelOpen(gameObject.GetComponent<Image>().sprite,idProgramm);
        }
    }
}
