using UnityEngine;

public class RayCasting : MonoBehaviour
{
    [SerializeField] private LayerMask interactiveObjectLayer;
    [SerializeField] private GameObject ruca;
    [SerializeField] private GameObject crest;
    void Update()
    {
        CheckingObjectTags();
    }
    private void CheckingObjectTags()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if(Physics.Raycast(ray, out _hit, 3f, interactiveObjectLayer))
        {
            switch(_hit.collider.gameObject.tag)
            {
                case "test":
                    ChangingTheCursor(true);
                break;
            }
        }
        else
        {
            ChangingTheCursor(false);
        }
    }
    private void ChangingTheCursor(bool _activ)
    {
        if(_activ==true)
        {
            ruca.SetActive(true);
            crest.SetActive(false);
        }
        if(_activ==false)
        {
            ruca.SetActive(false);
            crest.SetActive(true);
        }
    }
}
