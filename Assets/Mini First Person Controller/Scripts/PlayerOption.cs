using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerOption : MonoBehaviour
{
    [Header("Слои для взаимодействий с объектами")]
    [SerializeField] private LayerMask[] layerMasks;

    [Header("Курсор")]
    [SerializeField] private GameObject cursor;


    [Header("Управление")]
    public KeyCode takeAnObjectkey;//взять объект
    public KeyCode useObjectkey;//использовать объект
    public KeyCode putAnObjectkey;//поставить объект
    public KeyCode dropTheObject;//бросить объект
    public KeyCode turnTheObjectToTheLeft;//повернуть объект в лево
    public KeyCode turnTheObjectToTheRight;//повернуть объект в право

    private Transform armTransform;//трансформ руки
    [SerializeField] private Transform objectInHand;

    private bool checkingForTheTakenObject = false;//проверка на взятый объект
    private bool putAnObject = false;//объект установлен на землю
    private bool constructionMode = false;//режим строительства
    private bool liftingAHeavyObject = false;//проверка на задержиную кнопку при взятии большого объекта 
    private float timer; 


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
        if(constructionMode) return;//проверка на режим строительства
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if(Physics.Raycast(ray, out hit, 4, layerMasks[0]) && checkingForTheTakenObject == false)
        {
            //взять объект
            if(Input.GetKeyDown(takeAnObjectkey))
            {
                objectInHand = hit.transform;
                Tacing();
                EventBus.OnShowPlaces?.Invoke(objectInHand.tag);
                return;
            }  

            //использовать объект
            if(Input.GetKeyDown(useObjectkey))
            {
                if(hit.transform.GetComponent<CoffeePackage>())
                {
                    if(hit.transform.GetComponent<CoffeePackage>().packagingCondition)
                        hit.transform.GetComponent<CoffeePackage>().UnpackThePackage();
                    else
                        hit.transform.GetComponent<CoffeePackage>().PackAPackage();
                    return;
                }
            } 
        }

        //взаимодействуем с местом размещения объекта
        if(Physics.Raycast(ray, out hit, 4, layerMasks[4]) && checkingForTheTakenObject && putAnObject == false)
        {
            if(Input.GetKeyDown(takeAnObjectkey))
            {
                objectInHand.parent = hit.transform;
                objectInHand.transform.position = hit.transform.position;
                objectInHand.transform.rotation = hit.transform.rotation;
                objectInHand.localScale = new Vector3(1,1,1);
                Put();
                hit.transform.GetComponent<LocationForTheObject>().IsTheSpaceBeingUsed = true;
            }
        }
        
        
        if(checkingForTheTakenObject  && objectInHand != null && putAnObject == false)
        {
            //бросить объект
            if(Input.GetKeyDown(dropTheObject))
            {
                objectInHand.GetComponent<InteractiveObject>().DropTheObject();
                objectInHand.parent = null;
                Put();
                return;
            }

            //установить объект
            if(Input.GetKeyDown(putAnObjectkey))
            {
                Debug.Log("!!1");
                objectInHand.parent = null;
                putAnObject = true;
                objectInHand.rotation = Quaternion.Euler(0,0,0);
                EventBus.OnHideAPlace?.Invoke(objectInHand.tag);
                return;
            }
        }

        if(Physics.Raycast(ray, out hit, 4, layerMasks[2]) && checkingForTheTakenObject &&  putAnObject)
        {
            objectInHand.position = hit.point;

            //вращять объект на лево
            if(Input.GetKey(turnTheObjectToTheLeft))
            {
                objectInHand.Rotate(0,1,0);
            }
            //вращять на право
            if(Input.GetKey(turnTheObjectToTheRight))
            {
                objectInHand.Rotate(0,-1,0);
            }
            //оставить объект на земле
            if(Input.GetKeyDown(takeAnObjectkey))
            {
                objectInHand.GetComponent<InteractiveObject>().PutAnObject();
                putAnObject = false;
                Put();
                return;
            }
            // снова взять объект
            if(Input.GetKeyDown(putAnObjectkey))
            {
                Debug.Log("!!!1");
                Tacing();
                putAnObject = false;
                EventBus.OnShowPlaces?.Invoke(objectInHand.tag);
                return;
            }
        }
    }


    private void InteractionWithLargeObjects()
    {
        if(constructionMode == false) return;//проверка на режим строительства

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 4, layerMasks[3]) && checkingForTheTakenObject == false)
        {
            //подбор тяжёлого объекта
            if(Input.GetKeyDown(takeAnObjectkey))
            {
                liftingAHeavyObject = true;
            }
            //прерывание поднятие тяжёлого объекта
            if(Input.GetKeyUp(takeAnObjectkey))
            {
                liftingAHeavyObject = false;
                cursor.GetComponent<Image>().fillAmount = 1;
                timer = 0;
            }
            //активный таймер
            if(liftingAHeavyObject)
            {
                timer += 1*Time.deltaTime;
                cursor.GetComponent<Image>().fillAmount = timer;
                if(timer >1)
                {
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

            //вращять объект в лево
            if(Input.GetKey(turnTheObjectToTheLeft))
            {
                objectInHand.Rotate(0,1,0);
            }
            //вращять объект в право
            if(Input.GetKey(turnTheObjectToTheRight))
            {
                objectInHand.Rotate(0,-1,0);
            }
            //оставить объект на земле
            if(Input.GetKeyDown(takeAnObjectkey))
            {
                objectInHand.GetComponent<InteractiveObject>().PutAnObject();
                putAnObject = false;
                Put();
                return;
            }
            //взять снова объект с земли
            if(Input.GetKeyDown(putAnObjectkey))
            {
                Tacing();
                putAnObject = false;
                return;
            }
        }
        
  }

    //взять
    private void Tacing()
    {
        objectInHand.position = armTransform.position;//позиция объекта = позиции руки
        objectInHand.parent = armTransform;//объект становится ребёнком объекта
        objectInHand.rotation = armTransform.rotation;//объект становится в позицию вращения 0
        objectInHand.transform.GetComponent<InteractiveObject>().TakingObject();//вызываем метод отвечающий за физику
        checkingForTheTakenObject = true;//указываем на то что объект в руках
    }

    //положить объект 
    private void Put()
    {
        //objectInHand.position = hit.position;
        EventBus.OnHideAPlace?.Invoke(objectInHand.tag);
        checkingForTheTakenObject = false;
        objectInHand = null;
    }
}
