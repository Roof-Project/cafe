using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RayCasting : MonoBehaviour
{
    [Header("Настройка управление взаимодействия")]
    public KeyCode touch;
    public KeyCode use;
    public KeyCode rotateToTheLeft;
    public KeyCode rotateToTheRight;

    [Header("Настройка курсоров")]
    [SerializeField] private GameObject arm;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject theScaleOfLoadingTakingAnItem;
    [SerializeField] private GameObject user;

    [Header("Настройка слоя интерактивных объектов")]
    [SerializeField] private LayerMask[] interactiveObjectLayer;

    [Header("Настройка шейдера для взятых объектов")]
    [SerializeField] private Material shaderOfTheTakenObject;
    
    [Header("Места размещения стаканчика")]
    [SerializeField] private Transform[] placesOfGlasses;
    [Header("Места размещения лотка кофемолки")]
    public static List<Transform> placesForTheTray = new List<Transform>();
    [Header("Маста для размещения крышки")]
    [SerializeField] private Transform[] placeForTheLid;
    [Header("Место для размещения покета с кофе")]
    public static List<Transform> placeForACoffeeBag = new List<Transform>();
    [Header("Место для размещения турки")]
    public static List<Transform> placeForTurks = new List<Transform>();

    private string objectTagInHand;
    private GameObject interactiveObject;
    private static Vector3 hit;
    private bool activInteratbl=false;
    [SerializeField] private bool constructionMode=false;//отвечает за режимы переноса объектов большой/маленький
    private bool objectInHand=false;//предмет в руке
    private bool coffeeGrinderOnStage;
    [SerializeField] private byte numberLayer = 2;
    [SerializeField] private float RotationSpeed;
    [SerializeField] Transform armController;
    private Transform newObject;//храним объект который взяли

    private void Update()
    {
        CheckingObjectTags();
    }
    private void Awake() 
    {
        if(GameObject.FindObjectOfType<Coffemolca>())
            coffeeGrinderOnStage = true;
    }

    //пускает лучи пока не пройдёт проверка по тегу 
    private void CheckingObjectTags()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if(Physics.Raycast(ray, out _hit, 3f, interactiveObjectLayer[numberLayer]))
        {
            if(constructionMode==true)//условие для мебели
                hit=_hit.point;
                switch(_hit.collider.gameObject.tag)
                {
                    case "test":
                        СursorСontroller(true);
                        ProcessingTheInteractionOfObjects(_hit.collider);
                    break;
                }
            if(constructionMode==false&&(objectTagInHand == _hit.collider.tag || objectTagInHand == null))//условие дял маленьких объектов
            {
                СursorСontroller(true);
                TakingAnObjectInHand(_hit.transform);
            }
            return;
        }
        if(Physics.Raycast(ray, out _hit, 3f, interactiveObjectLayer[3]))
        {
            СursorСontroller(true);
            Hint(true);
            UsingItems(_hit.collider);
            return;
        }
        else
        {
            СursorСontroller(false);
            Hint(false);
            return;
        }
    }
    private void TakingAnObjectInHand(Transform _object)
    {
        if(Input.GetKeyDown(touch))
        {
            if(objectInHand == false)
            {
                newObject = _object;
                newObject.GetComponent<Collider>().isTrigger = true;
                if(newObject.GetComponent<Rigidbody>())
                    newObject.GetComponent<Rigidbody>().isKinematic = true;
                newObject.position = armController.position;
                newObject.rotation = Quaternion.Euler(0,0,0);
                newObject.transform.SetParent(armController.transform);
                switch(newObject.gameObject.tag)
                {
                    case "glass":
                        for(int i = 0; i <= placesOfGlasses.Length-1; i++)
                        {
                            placesOfGlasses[i].gameObject.SetActive(true);
                            placesOfGlasses[i].GetComponent<InteractiveObject>().interactiveObjectInPlace = false;
                        }
                    break;
                    case "taracoffemolci":
                        for(byte i = 0; i <= placesForTheTray.Count -1; i++)
                        {
                            placesForTheTray[i].gameObject.SetActive(true);
                            placesForTheTray[i].GetComponent<InteractiveObject>().interactiveObjectInPlace = false;
                        }
                    break;
                    case "cuplid":
                        for(byte i = 0; i <= placeForTheLid.Length -1; i++)
                        {
                            placeForTheLid[i].gameObject.SetActive(true);
                            placeForTheLid[i].GetComponent<InteractiveObject>().interactiveObjectInPlace = false;
                        }
                    break;
                    case "coffePac":
                        for(byte i = 0; i <= placeForACoffeeBag.Count -1; i++)
                        {
                            placeForACoffeeBag[i].gameObject.SetActive(true);
                            placeForACoffeeBag[i].GetComponent<InteractiveObject>().interactiveObjectInPlace = false;
                        }
                        if(coffeeGrinderOnStage)
                            Coffemolca.CheckingThePresenceOfTheCoffeeGrinderTray();
                    break;
                    case "turka":
                        for(byte i = 0; i <= placeForTurks.Count -1; i++)
                        {
                            placeForTurks[i].gameObject.SetActive(true);
                            placeForTurks[i].GetComponent<InteractiveObject>().interactiveObjectInPlace = false;
                        }
                    break;
                }
                objectTagInHand = newObject.tag;
                objectInHand = true;
                return;
            }
            if(objectInHand == true)
            {
                newObject.GetComponent<Collider>().isTrigger = false;
                if(newObject.GetComponent<Rigidbody>())
                    newObject.GetComponent<Rigidbody>().isKinematic = false;
                newObject.SetParent(_object.transform.parent);
                newObject.position = _object.position;
                newObject.rotation = _object.rotation;
                _object.GetComponent<InteractiveObject>().interactiveObjectInPlace = true;
                switch(_object.gameObject.tag)
                {
                    case "glass":
                        for(int i = 0; i <= placesOfGlasses.Length-1; i++)
                        {
                            placesOfGlasses[i].gameObject.SetActive(false);
                        }
                    break;
                    case "taracoffemolci":
                        for(byte i = 0; i <= placesForTheTray.Count -1; i++)
                        {
                            placesForTheTray[i].gameObject.SetActive(false);
                        }
                    break;
                    case "cuplid":
                        for(byte i = 0; i <= placeForTheLid.Length-1; i++)
                        {
                            placeForTheLid[i].gameObject.SetActive(false);
                        }
                    break;
                    case "coffePac":
                        for(byte i = 0; i <= placeForACoffeeBag.Count -1; i++)
                        {
                            placeForACoffeeBag[i].gameObject.SetActive(false);
                        }
                    break;
                    case "turka":
                        for(byte i = 0; i <= placeForTurks.Count-1; i++)
                        {
                            placeForTurks[i].gameObject.SetActive(false);
                        }
                    break;
                }
                objectTagInHand = null;
                objectInHand = false;
                return;
            }
        } 
    }

    private void UsingItems(Collider _object)
    {
        if(Input.GetKeyDown(use))
        {
            switch(_object.tag)
            {
                case "plateButton": 
                    Stove.TurningOnAndOffTheStove();
                break;
                case "tapHandle":
                    Crane.UseTheFaucet();
                break;
            }
            return;
        }
    }


    private void Hint(bool _activ)
    {
        if(_activ==true)
            user.GetComponent<Animator>().Play("ECoursorAnimStart");
        if(_activ==false)
            user.GetComponent<Animator>().Play("ECoursorAnimEnd");
    }
    //регулирует курсор в зависимосте от объекта на который мы навелись 
    private void СursorСontroller(bool _activ)
    {
        if(_activ==true && arm.activeSelf==false)
        {
            arm.SetActive(true);
            cursor.SetActive(false);
            return;
        }
        if(_activ==false && arm.activeSelf==true)
        {
            arm.SetActive(false);
            cursor.SetActive(true);
            return;
        }
        return;
    }

    //Обработка Взаимодействия Объектов
    private void ProcessingTheInteractionOfObjects(Collider _other)
    {
        if(Input.GetKey(touch) && !activInteratbl)
        {
            
            theScaleOfLoadingTakingAnItem.GetComponent<Image>().fillAmount += Time.deltaTime/2.5f;
            if(theScaleOfLoadingTakingAnItem.GetComponent<Image>().fillAmount>=1)
            {
                
                interactiveObject = _other.gameObject;
                StartCoroutine(TransferringInteractiveObjects());
                numberLayer++;
                theScaleOfLoadingTakingAnItem.GetComponent<Image>().fillAmount = 0;
                interactiveObject.GetComponent<Collider>().enabled=false;
                activInteratbl=!activInteratbl;
            }
        }
        else
        {
            theScaleOfLoadingTakingAnItem.GetComponent<Image>().fillAmount = 0;
        }
    }

    //Установка объекта
    private void ObjectInstallation()
    {
        StopCoroutine(TransferringInteractiveObjects());
        //interactiveObject.returnTheOriginalMaterial();
        numberLayer--;
        interactiveObject.GetComponent<Collider>().enabled=true;
        interactiveObject = null;
        activInteratbl =! activInteratbl;
    }

    //вращение объектов 
    private void ObjectRotation()
    {
        if(Input.GetKey(rotateToTheLeft))
            interactiveObject.transform.Rotate(0,RotationSpeed,0);
        if(Input.GetKey(rotateToTheRight))
            interactiveObject.transform.Rotate(0,-RotationSpeed,0);
    }

    //Передача интерактивных объектов
    private IEnumerator TransferringInteractiveObjects()
    {
        while(true)
        {
            interactiveObject.transform.position = hit;
            ObjectRotation();
            if(Input.GetKeyDown(touch) && activInteratbl && interactiveObject!=null)
            {
                ObjectInstallation();
                yield break;
            }
            yield return null;
        }
    }
}
