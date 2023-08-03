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
    [SerializeField] private GameObject indicator;


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
    
    [SerializeField] Camera _camera;


    void Start()
    {
        armTransform = transform.GetChild(0).GetChild(0);
        //StartCoroutine(UsedItem());
    }

    void Update()
    {
        InteractionWithSmallObjects();
        InteractionWithLargeObjects();
    }

    private IEnumerator UsedItem()
    {
        while(true)
        {
            InteractionWithSmallObjects();
            InteractionWithLargeObjects();
            yield return null;
        }
    }

    private void InteractionWithSmallObjects()//взаимодействие с маленькими объектами
    {
        if(constructionMode) return;//проверка на режим строительства

            //взять объект
            if(Input.GetKeyDown(takeAnObjectkey))
            {
                Ray _ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit _hit;
                if(Physics.Raycast(_ray, out _hit, 4, layerMasks[0]) && checkingForTheTakenObject == false)
                {
                    objectInHand = _hit.transform;
                    Tacing();
                    EventBus.OnShowPlaces?.Invoke(objectInHand.tag);
                    return;
                }
            }  

            //использовать объект
            if(Input.GetKeyDown(useObjectkey))
            {
                Ray _ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit _hit;
                if(Physics.Raycast(_ray, out _hit, 4, layerMasks[0]) && checkingForTheTakenObject == false)
                {
                    if(_hit.transform.GetComponent<CoffeePackage>())
                    {
                        if(_hit.transform.GetComponent<CoffeePackage>().packagingCondition)
                            _hit.transform.GetComponent<CoffeePackage>().UnpackThePackage();
                        else
                            _hit.transform.GetComponent<CoffeePackage>().PackAPackage();
                        return;
                    }
                }
            } 

        //взаимодействуем с местом размещения объекта
        
            if(Input.GetKeyDown(takeAnObjectkey))
            {
                Ray _ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit _hit;
                if(Physics.Raycast(_ray, out _hit, 4, layerMasks[4]) && checkingForTheTakenObject && putAnObject == false)
                {
                    objectInHand.parent = _hit.transform;
                    objectInHand.transform.position = _hit.transform.position;
                    objectInHand.transform.rotation = _hit.transform.rotation;
                    objectInHand.localScale = new Vector3(1,1,1);
                    Put();
                    _hit.transform.GetComponent<LocationForTheObject>().IsTheSpaceBeingUsed = true;
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
        
        //объект устанавливается 
        if(checkingForTheTakenObject &&  putAnObject)
        {
            Ray _ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;
            if(Physics.Raycast(_ray, out _hit, 4, layerMasks[2]))
            {
                objectInHand.position = _hit.point;

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
    }


    private void InteractionWithLargeObjects()
    {
        if(constructionMode == false) return;//проверка на режим строительства
            //подбор тяжёлого объекта
        if(Input.GetKeyDown(takeAnObjectkey))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 4, layerMasks[3]) && checkingForTheTakenObject == false)
                liftingAHeavyObject = true;

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
        //прерывание поднятие тяжёлого объекта
        if(Input.GetKeyUp(takeAnObjectkey))
        {
            liftingAHeavyObject = false;
            cursor.GetComponent<Image>().fillAmount = 1;
            timer = 0;
        }
        

        if(Input.GetKeyDown(putAnObjectkey) && checkingForTheTakenObject && objectInHand != null && putAnObject == false)
        {
            objectInHand.parent = null;
            putAnObject = true;
            objectInHand.rotation = Quaternion.Euler(0,0,0);
            return;
        }
        //объект устанавливается 
        if(checkingForTheTakenObject &&  putAnObject)
        {
            Ray _ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;
            if(Physics.Raycast(_ray, out _hit, 4, layerMasks[2]))
            {
                objectInHand.position = _hit.point;

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
