using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class InputButton : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    public void StartCorutine()
    {
        inputManager.GetComponent<InputManager>().StartCoroutine("SetButton", gameObject);
    }    
}
