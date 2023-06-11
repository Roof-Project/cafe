using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Turca : MonoBehaviour
{
    private static Slider slider;
    private static GameObject Canvas;
    private static GameObject waterUi;
    private static GameObject CoffeeUi;
    private static GameObject Player;
    private static Camera Camera;
    private static TextMeshProUGUI textMeshProUGUI;
    public static bool water;
    public static bool coffee;
    public static bool TheTurkIsEmpty = true;
    public static float _water;
    //public static float _coffee;
    //static float _waterFull;
    //static float _coffeeFull;
    
    private void Start() 
    {
        Canvas = transform.GetChild(1).gameObject;
        waterUi = transform.GetChild(1).GetChild(0).gameObject;
        slider = transform.GetChild(1).GetChild(1).GetComponent<Slider>();
        textMeshProUGUI = waterUi.transform.GetChild(0).GetComponent<TextMeshProUGUI>(); 
        Player = GameObject.Find("First Person Controller Minimal");
        Camera = Player.transform.GetChild(0).GetComponent<Camera>();
        StartCoroutine(UiUpdater());
    }

    private static void WaterFilling()
    {
        if(waterUi.activeSelf==false)
        {
            waterUi.gameObject.SetActive(true);
            slider.gameObject.SetActive(true);
        }
        if(textMeshProUGUI.text != "вода")
            textMeshProUGUI.text = "вода";
    }
    public static void FillingTurks()
    {
        if(slider.value < slider.maxValue)
        {
            if(water == false)
            {
                WaterFilling();
                water = true;
            }
            _water += 0.2f;
            slider.value = _water;
        }
        if(slider.value >= slider.maxValue)
        {
            slider.gameObject.SetActive(false);
            TheTurkIsEmpty = false;
        }
    }
    private IEnumerator UiUpdater()
    {
        while(true)
        {
            if(Player.activeSelf && (water || coffee))
            {
                Canvas.transform.LookAt(Camera.transform);
            }
            yield return null;
        }
    }
}
