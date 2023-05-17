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
    [SerializeField] private LayerMask interactiveObjectLayer;

    [Header("Настройка шейдера для взятых объектов")]
    [SerializeField] private Material shaderOfTheTakenObject;

    void Update()
    {
        CheckingObjectTags();
    }

    //пускает лучи пока не пройдёт проверка по тегу 
    private void CheckingObjectTags()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if(Physics.Raycast(ray, out _hit, 3f, interactiveObjectLayer))
        {
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
        if(Input.GetKey(use))
        {
            theScaleOfLoadingTakingAnItem.GetComponent<Image>().fillAmount += Time.deltaTime/5;
        }
        else
        {
            theScaleOfLoadingTakingAnItem.GetComponent<Image>().fillAmount = 0;
        }
    }
}
