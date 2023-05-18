using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private static Material originalMaterial;

    private void Start()
    {
        originalMaterial = GetComponent<MeshRenderer>().material;
    }
    public void returnTheOriginalMaterial()
    {
        GetComponent<MeshRenderer>().material = originalMaterial;
    }
}
