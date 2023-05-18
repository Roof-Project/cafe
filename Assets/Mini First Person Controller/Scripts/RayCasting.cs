using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class RayCasting : MonoBehaviour
{
    [Header("Настройка управление взаимодействия")]
    public KeyCode use;
    public KeyCode rotateToTheLeft;
    public KeyCode rotateToTheRight;

    [Header("Настройка курсоров")]
    [SerializeField] private GameObject arm;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject theScaleOfLoadingTakingAnItem;

    [Header("Настройка слоя интерактивных объектов")]
    [SerializeField] private LayerMask[] interactiveObjectLayer;

    [Header("Настройка шейдера для взятых объектов")]
    [SerializeField] private Material shaderOfTheTakenObject;

    private InteractiveObject interactiveObject;
    private static Vector3 hit;
    private bool activInteratbl=false;
    private static byte numberLayer;
    [SerializeField] private float RotationSpeed;
    [SerializeField] Transform armController;


    void Update()
    {
        CheckingObjectTags();
    }

    //пускает лучи пока не пройдёт проверка по тегу 
    private void CheckingObjectTags()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if(Physics.Raycast(ray, out _hit, 3f, interactiveObjectLayer[numberLayer]))
        {
            hit=_hit.point;
            switch(_hit.collider.gameObject.tag)
            {
                case "test":
                    СursorСontroller(true);
                    ProcessingTheInteractionOfObjects(_hit.collider);
                break;
            }
        }
        else
        {
            СursorСontroller(false);
        }
    }

    //регулирует курсор в зависимосте от объекта на который мы навелись 
    private void СursorСontroller(bool _activ)
    {
        if(_activ==true)
        {
            arm.SetActive(true);
            cursor.SetActive(false);
        }
        if(_activ==false)
        {
            arm.SetActive(false);
            cursor.SetActive(true);
        }
    }

    //Обработка Взаимодействия Объектов
    private void ProcessingTheInteractionOfObjects(Collider _other)
    {
        if(Input.GetKey(use) && !activInteratbl)
        {
            
            theScaleOfLoadingTakingAnItem.GetComponent<Image>().fillAmount += Time.deltaTime/2.5f;
            if(theScaleOfLoadingTakingAnItem.GetComponent<Image>().fillAmount>=1)
            {
                
                interactiveObject = _other.GetComponent<InteractiveObject>();
                interactiveObject.GetComponent<MeshRenderer>().material=shaderOfTheTakenObject;
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
        interactiveObject.returnTheOriginalMaterial();
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
            interactiveObject.transform.position = hit + new Vector3(0,0.5f,0);
            ObjectRotation();
            if(Input.GetKeyDown(use) && activInteratbl && interactiveObject!=null)
            {
                ObjectInstallation();
                yield break;
            }
            yield return null;
        }
    }
}
