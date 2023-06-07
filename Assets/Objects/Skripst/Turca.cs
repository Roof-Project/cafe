using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Turca : MonoBehaviour
{
    private static Slider slider;
    private static GameObject waterUi;
    private static TextMeshProUGUI textMeshProUGUI;
    public static bool water;
    public static bool coffee;
    public static float _water;
    public static float _coffee;
    static float _waterFull;
    static float _coffeeFull;
    
    private void Start() 
    {
        waterUi = transform.GetChild(1).GetChild(0).gameObject;
        slider = waterUi.transform.GetChild(0).GetComponent<Slider>();
        textMeshProUGUI = waterUi.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        //StartCoroutine(UiUpdater()); 
    }

    public void WaterFilling()
    {
        if(waterUi.activeSelf==false)
            waterUi.gameObject.SetActive(true);
        if(textMeshProUGUI.text != "вода")
            textMeshProUGUI.text = "вода";
    }
    private IEnumerator UiUpdater()
    {
        while(true)
        {
            slider.transform.parent.LookAt(Camera.main.transform.position);
            if(Crane.turkOnTheSpot && slider.value < slider.maxValue)
            {
                if(water == false)
                {
                    WaterFilling();
                    water = true;
                }
                _water += 0.2f;
                slider.value = _water;
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }
    } 
}
