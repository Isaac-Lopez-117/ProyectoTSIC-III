using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButtonManager : MonoBehaviour
{
    // Variables para almacenar los datos del elemento
    private string itemName;
    private string itemDescription;
    private Sprite itemImage;
    private GameObject item3DModel;
    private ARInteractionsManager interactionsManager;

    // Propiedades para asignar los valores de los datos del elemento desde otros scripts
    public string ItemName{ set { itemName = value; } }
    public string ItemDescription{ set { itemDescription = value; } }
    public Sprite ItemImage{ set { itemImage = value; } }
    public GameObject Item3DModel{ set { item3DModel = value; } }

    // Start es llamado antes del primer fotograma
    void Start()
    {
        // Asigna los valores de nombre, imagen y descripción del elemento a los elementos de la interfaz de usuario correspondientes
        transform.GetChild(0).GetComponent<TMP_Text>().text = itemName;
        transform.GetChild(1).GetComponent<RawImage>().texture = itemImage.texture;
        transform.GetChild(2).GetComponent<TMP_Text>().text = itemDescription;

        // Agrega un listener al botón para que llame a los métodos correspondientes cuando se haga clic en él
        var button = GetComponent<Button>();
        button.onClick.AddListener(GameManager.instance.ARPosition);
        button.onClick.AddListener(Create3DModel);

        // Encuentra la instancia de ARInteractionsManager en la escena
        interactionsManager = FindObjectOfType<ARInteractionsManager>();
    }

    // Crea un modelo 3D del elemento
    private void Create3DModel()
    {
        interactionsManager.Item3DModel = Instantiate(item3DModel);
    }
}
