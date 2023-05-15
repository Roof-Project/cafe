using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTime : MonoBehaviour
{
    public TextMeshProUGUI TimerMinut;
    public TextMeshProUGUI TimerHours;
    public float TimeGameMinets = 0;
    public float TimeGameHaurs = 0;
    void Start()
    {
        StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        while (true)
        {
            TimeGameMinets += Time.deltaTime / 10;
            if (TimeGameMinets < 10)
            {
                TimerMinut.text = "0" + (int)TimeGameMinets;
            }
            else
                TimerMinut.text = "" + (int)TimeGameMinets;
            if (TimeGameMinets >= 60)
            {
                TimeGameHaurs++;
                TimeGameMinets = 0;
                TimerHours.text = "" + (int)TimeGameHaurs;
            }
            if (TimeGameHaurs >= 24)
                TimeGameHaurs = 0;
            yield return null;
        }
    }
    


}
