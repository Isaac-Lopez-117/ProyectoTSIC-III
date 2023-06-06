using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // Eventos que pueden ser suscritos por otros scripts
    public event Action OnWelcomeMenu;
    public event Action OnMainMenu;
    public event Action OnItemsMenu;
    public event Action OnARPosition;

    // Instancia del GameManager para acceder desde otros scripts
    public static GameManager instance;

    private void Awake()
    {
        // Comprueba si ya hay una instancia del GameManager y destruye este objeto si es así
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start es llamado antes del primer fotograma
    void Start()
    {
        // Inicia el menú de bienvenida
        WelcomeMenu();
    }

    // Activa el menú de bienvenida
    public void WelcomeMenu()
    {
        // Invoca el evento OnWelcomeMenu si hay suscriptores y muestra un mensaje de depuración
        OnWelcomeMenu?.Invoke();
        Debug.Log("Welcome Menu Activated");
    }

    // Activa el menú principal
    public void MainMenu()
    {
        // Invoca el evento OnMainMenu si hay suscriptores y muestra un mensaje de depuración
        OnMainMenu?.Invoke();
        Debug.Log("Main Menu Activated");
    }

    // Activa el menú de elementos
    public void ItemsMenu()
    {
        // Invoca el evento OnItemsMenu si hay suscriptores y muestra un mensaje de depuración
        OnItemsMenu?.Invoke();
        Debug.Log("Items Menu Activated");
    }

    // Activa la posición de realidad aumentada
    public void ARPosition()
    {
        // Invoca el evento OnARPosition si hay suscriptores y muestra un mensaje de depuración
        OnARPosition?.Invoke();
        Debug.Log("AR Position Activated");
    }

    // Cierra la aplicación
    public void CloseApp()
    {
        Application.Quit();
    }
}
