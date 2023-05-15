using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    [SerializeField]
    int id;
    public void asd()
    {
        WindowOFF.windowOFF.runningProgram[id].SetActive(true);
    }
}