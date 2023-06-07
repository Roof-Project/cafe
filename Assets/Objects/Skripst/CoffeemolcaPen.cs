using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class help : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    private static Vector3 mousePosition;
    private static Transform grip; 
    private static bool drag;
    
    public void OnPointerUp(PointerEventData eventData)
    {
        drag = false;
        StopCoroutine(Twist());
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        drag = true;
        StartCoroutine(Twist());
    }
    private void Start() 
    {
        grip = transform;
        
    }
    public static IEnumerator Twist()
    {
        while(true)
        {
            if(drag)
            {
                mousePosition = new Vector2(Screen.width / 2  - Input.mousePosition.x , Screen.height / 2 - Input.mousePosition.y) ; 
                Vector3 difference = mousePosition -grip.position;
                difference.Normalize();
                float rotation_z = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
                grip.rotation = Quaternion.Euler(-90, 0f, rotation_z+90);
                yield return null;
            }
            yield return null; 
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Debug.Log("!!!!!!!!!!");
    }
}

