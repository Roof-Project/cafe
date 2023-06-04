using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turca : MonoBehaviour
{
    private static Slider waterSlider;
    public static bool water;
    public static bool coffee;
    public static float _water;
    public static float _coffee;
    static float _waterFull;
    static float _coffeeFull;
    
    private void Start() 
    {
        waterSlider = transform.GetChild(1).GetChild(0).GetComponent<Slider>();
        StartCoroutine(UiUpdater());    
    }

    public static void WaterFilling()
    {
        waterSlider.gameObject.SetActive(true);
    }
    private IEnumerator UiUpdater()
    {
        while(true)
        {
            waterSlider.transform.LookAt(Camera.main.transform.position);
            yield return null;
        }
    } 
}
