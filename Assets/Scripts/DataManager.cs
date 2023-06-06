using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Lista de objetos (items)
    [SerializeField] private List<Item> items = new List<Item>();

    // Contenedor de botones
    [SerializeField] private GameObject buttonContainer;

    // Administrador de botones de items
    [SerializeField] private ItemButtonManager itemButtonManager;

    // Método Start se llama antes del primer fotograma
    void Start()
    {
        // Suscribirse al evento de mostrar el menú de items en el GameManager
        GameManager.instance.OnItemsMenu += CreateButtons;
    }

    // Crear botones para cada item en la lista
    private void CreateButtons()
    {
        // Para cada item en la lista de items
        foreach (var item in items)
        {
            // Instanciar un nuevo botón de item utilizando el administrador de botones
            ItemButtonManager itemButton;
            itemButton = Instantiate(itemButtonManager, buttonContainer.transform);

            // Asignar los atributos del item al botón
            itemButton.ItemName = item.ItemName;
            itemButton.ItemDescription = item.ItemDescription;
            itemButton.ItemImage = item.ItemImage;
            itemButton.Item3DModel = item.Item3DModel;
            itemButton.name = item.ItemName;
        }

        // Desuscribirse del evento para evitar la creación duplicada de botones
        GameManager.instance.OnItemsMenu -= CreateButtons;
    }
}
