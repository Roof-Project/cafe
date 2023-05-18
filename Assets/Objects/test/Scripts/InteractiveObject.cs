using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public static Material originalMaterial;

    private void Start()
    {
        originalMaterial = GetComponent<MeshRenderer>().material;
    }
}
