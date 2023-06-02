using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePart : MonoBehaviour
{
    public GameObject superiorComplejo;
    public GameObject superiorSimple;
    public GameObject inferiorComplejo;
    public GameObject inferiorSimple;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (superiorComplejo.transform.localScale == Vector3.zero)
        {
             superiorSimple.transform.localScale = Vector3.one;
        }

        if (superiorSimple.transform.localScale == Vector3.zero)
        {
            superiorComplejo.transform.localScale = Vector3.one;
        }

        if (inferiorComplejo.transform.localScale == Vector3.zero)
        {
            inferiorSimple.transform.localScale = Vector3.one;
        }

        if (inferiorSimple.transform.localScale == Vector3.zero)
        {
            inferiorComplejo.transform.localScale = Vector3.one;
        }

    }
}
