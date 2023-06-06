using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Se utiliza para permitir la creación de instancias de este scriptable object desde el menú de Assets en Unity.
[CreateAssetMenu]
public class Item : ScriptableObject
{
    // Nombre del objeto
    public string ItemName;

    // Imagen del objeto
    public Sprite ItemImage;

    // Descripción del objeto
    public string ItemDescription;

    // Modelo 3D del objeto
    public GameObject Item3DModel;
}
