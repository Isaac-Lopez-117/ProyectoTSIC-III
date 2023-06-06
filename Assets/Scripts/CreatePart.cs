using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePart : MonoBehaviour
{
    // Variables públicas para asignar objetos en el Inspector de Unity
    public GameObject superiorComplejo;  // Objeto de parte superior compleja
    public GameObject superiorSimple;    // Objeto de parte superior simple
    public GameObject inferiorComplejo;  // Objeto de parte inferior compleja
    public GameObject inferiorSimple;    // Objeto de parte inferior simple

    // Update is called once per frame
    void Update()
    {
        // Este método se llama una vez por cada fotograma del juego

        // Si la escala de la parte superior compleja es cero
        if (superiorComplejo.transform.localScale == Vector3.zero)
        {
            // Establecer la escala de la parte superior simple a uno
            superiorSimple.transform.localScale = Vector3.one;
        }

        // Si la escala de la parte superior simple es cero
        if (superiorSimple.transform.localScale == Vector3.zero)
        {
            // Establecer la escala de la parte superior compleja a uno
            superiorComplejo.transform.localScale = Vector3.one;
        }

        // Si la escala de la parte inferior compleja es cero
        if (inferiorComplejo.transform.localScale == Vector3.zero)
        {
            // Establecer la escala de la parte inferior simple a uno
            inferiorSimple.transform.localScale = Vector3.one;
        }

        // Si la escala de la parte inferior simple es cero
        if (inferiorSimple.transform.localScale == Vector3.zero)
        {
            // Establecer la escala de la parte inferior compleja a uno
            inferiorComplejo.transform.localScale = Vector3.one;
        }
    }
}
