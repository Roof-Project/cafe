using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerOption : MonoBehaviour
{
    [SerializeField] private LayerMask[] layerMasks;

    [Header("Курсор")]
    [SerializeField] private GameObject cursor;


    [Header("Управление")]
    public KeyCode takeAnObjectkey;
    public KeyCode useObjectkey;
    public KeyCode putAnObjectkey;
    public KeyCode turnTheObjectToTheLeft;
    public KeyCode turnTheObjectToTheRight;

    private Transform armTransform;
    [SerializeField] private Transform objectInHand;

    private bool checkingForTheTakenObject = false;
    private bool putAnObject = false;
    private bool constructionMode = true;//режим строительства
    private bool liftingAHeavyObject = false;//проверка на задержиную кнопку при взятии большого объекта 
    private float timer; 

    public delegate void toTake();
    public event toTake takeABagOfCoffee;
    void Start()
    {
        armTransform = transform.GetChild(0).GetChild(0);
    }

    void Update()
    {
        InteractionWithSmallObjects();
        InteractionWithLargeObjects();
    }

    private void InteractionWithSmallObjects()//взаимодействие с маленькими объектами
    {
        if(constructionMode == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //взять объект
            if(Physics.Raycast(ray, out hit, 4, layerMasks[0]) && checkingForTheTakenObject == false)
            {
                if(Input.GetKeyDown(takeAnObjectkey))
                {
                    objectInHand = hit.transform;
                    Tacing();
                    checkingForTheTakenObject = true;
                    switch(hit.collider.tag)
                    {
                        case "CoffeePackage":
                            takeABagOfCoffee?.Invoke();
                        break;
                    }
                    return;
                }    
            }

            //установить объект
            if(Physics.Raycast(ray, out hit, 4, layerMasks[1]) && checkingForTheTakenObject)
            {
                if(Input.GetKeyDown(takeAnObjectkey))
                {
                    //под вопросом
                    Put();

                }    
            }

            //положить объект 
            if(Input.GetKeyDown(putAnObjectkey) && checkingForTheTakenObject && objectInHand != null && putAnObject == false)
            {
                objectInHand.parent = null;
                putAnObject = true;
                objectInHand.rotation = Quaternion.Euler(0,0,0);
                return;
            }
            if(Physics.Raycast(ray, out hit, 4, layerMasks[2]) && checkingForTheTakenObject &&  putAnObject)
            {
                objectInHand.position = hit.point;

                if(Input.GetKey(turnTheObjectToTheLeft))
                {
                    objectInHand.Rotate(0,1,0);
                }
                if(Input.GetKey(turnTheObjectToTheRight))
                {
                    objectInHand.Rotate(0,-1,0);
                }
                if(Input.GetKeyDown(takeAnObjectkey))
                {
                    Put();
                    putAnObject = false;
                }
                if(Input.GetKeyDown(putAnObjectkey))
                {
                    Tacing();
                    putAnObject = false;
                }
            }
        }
    }


    private void InteractionWithLargeObjects()
    {
        if(constructionMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 4, layerMasks[3]) && checkingForTheTakenObject == false)
            {
                if(Input.GetKeyDown(takeAnObjectkey))
                {
                    liftingAHeavyObject = true;
                }
                if(Input.GetKeyUp(takeAnObjectkey))
                {
                    liftingAHeavyObject = false;
                    cursor.GetComponent<Image>().fillAmount = 1;
                    timer = 0;
                }
                if(liftingAHeavyObject)
                {
                    timer += 1*Time.deltaTime;
                    cursor.GetComponent<Image>().fillAmount = timer;
                    if(timer >1)
                    {
                        Debug.Log("!");
                        objectInHand = hit.transform;
                        Tacing();
                        timer = 0;
                        return;
                    }
                    
                }
            }


            if(Input.GetKeyDown(putAnObjectkey) && checkingForTheTakenObject && objectInHand != null && putAnObject == false)
            {
                objectInHand.parent = null;
                putAnObject = true;
                objectInHand.rotation = Quaternion.Euler(0,0,0);
                return;
            }
            if(Physics.Raycast(ray, out hit, 4, layerMasks[2]) && checkingForTheTakenObject &&  putAnObject)
            {
                objectInHand.position = hit.point;

                if(Input.GetKey(turnTheObjectToTheLeft))
                {
                    objectInHand.Rotate(0,1,0);
                }
                if(Input.GetKey(turnTheObjectToTheRight))
                {
                    objectInHand.Rotate(0,-1,0);
                }
                if(Input.GetKeyDown(takeAnObjectkey))
                {
                    Put();
                    putAnObject = false;
                }
                if(Input.GetKeyDown(putAnObjectkey))
                {
                    Tacing();
                    putAnObject = false;
                }
            }
        }
    }

    private void Tacing()
    {
        objectInHand.position = armTransform.position;
        objectInHand.parent = armTransform;
        objectInHand.rotation = Quaternion.Euler(0,0,0);
        objectInHand.transform.GetComponent<InteractiveObject>().TakingObject();
        checkingForTheTakenObject = true;
    }
    private void Put()
    {
        //objectInHand.position = hit.position;
        objectInHand.GetComponent<InteractiveObject>().PutAnObject();
        checkingForTheTakenObject = false;
        objectInHand = null;
        
    }
}
