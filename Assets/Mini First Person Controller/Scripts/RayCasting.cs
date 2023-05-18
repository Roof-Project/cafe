using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class RayCasting : MonoBehaviour
{
    [Header("Настройка управление взаимодействия")]
    public KeyCode use;

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
    private static bool activInteratbl=false;
    private static byte numberLayer;
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
    private void ProcessingTheInteractionOfObjects(Collider _other)
    {
        if(Input.GetKey(use)&&activInteratbl==false)
        {
            
            theScaleOfLoadingTakingAnItem.GetComponent<Image>().fillAmount += Time.deltaTime/5;
            if(theScaleOfLoadingTakingAnItem.GetComponent<Image>().fillAmount>=1)
            {
                
                interactiveObject = _other.GetComponent<InteractiveObject>();
                interactiveObject.GetComponent<MeshRenderer>().material=shaderOfTheTakenObject;
                StartCoroutine(TransferringInteractiveObjects());
                numberLayer++;
                theScaleOfLoadingTakingAnItem.GetComponent<Image>().fillAmount = 0;
                activInteratbl=!activInteratbl;
            }
        }
        else
        {
            theScaleOfLoadingTakingAnItem.GetComponent<Image>().fillAmount = 0;
        }
    }

    private IEnumerator TransferringInteractiveObjects()
    {
        while(true)
        {
            interactiveObject.transform.position = hit;
            yield return null;
        }
    }
}
