using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class ARInteractionsManager : MonoBehaviour
{
    // La cámara utilizada para la realidad aumentada
    [SerializeField] private Camera aRCamera;

    // El administrador de rayos de AR
    private ARRaycastManager aRRaycastManager;

    // Lista de colisiones de rayos de AR
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Objeto de puntero de AR
    private GameObject aRPointer;

    // Modelo 3D del objeto
    private GameObject item3DModel;

    // Objeto seleccionado
    private GameObject itemSelected;

    // Parte seleccionada del objeto 3D
    private GameObject partSelected;

    // Variable para verificar si está en la posición inicial
    private bool isInitialPosition;

    // Variable para verificar si se encuentra sobre una interfaz de usuario
    private bool isOverUI;

    // Variable para verificar si se encuentra sobre el modelo 3D
    private bool isOver3DModel;

    // Variable para verificar si se encuentra sobre una parte del modelo 3D
    public bool isOver3DPart;

    // Diferencia de posición táctil
    private Vector2 touchPositionDiff;


    // Propiedad para establecer el modelo 3D del objeto
    public GameObject Item3DModel
    {
        set
        {
            item3DModel = value;
            item3DModel.transform.position = aRPointer.transform.position;
            item3DModel.transform.parent = aRPointer.transform;
            isInitialPosition = true;
        }
    }

    // Se llama al inicio del script
    void Start()
    {
        // Obtiene el objeto ARPointer de los hijos del script
        aRPointer = transform.GetChild(0).gameObject;
        // Obtiene el ARRaycastManager del entorno
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        // Agrega un evento para establecer la posición inicial del objeto en el menú principal
        GameManager.instance.OnMainMenu += SetItemPosition;
    }

    // Se llama una vez por fotograma
    void Update()
    {
        // Si la posición inicial del objeto no se ha establecido
        if (isInitialPosition)
        {
            // Obtiene el punto medio de la pantalla
            Vector2 middlePointScreen = new Vector2(Screen.width / 2, Screen.height / 2);
            // Realiza un raycast desde el punto medio hacia los planos AR
            aRRaycastManager.Raycast(middlePointScreen, hits, TrackableType.Planes);
            if (hits.Count > 0)
            {
                // Establece la posición y rotación del objeto según el raycast
                transform.position = hits[0].pose.position;
                transform.rotation = hits[0].pose.rotation;
                aRPointer.SetActive(true);
                isInitialPosition = false;
            }
        }

        // Si hay al menos un toque en la pantalla
        if (Input.touchCount > 0)
        {
            Touch touchOne = Input.GetTouch(0);
            if (touchOne.phase == TouchPhase.Began)
            {
                var touchPosition = touchOne.position;
                // Comprueba si el toque está sobre la interfaz de usuario
                isOverUI = isTapOverUI(touchPosition);
                // Comprueba si el toque está sobre el modelo 3D
                isOver3DModel = isTapOver3DModel(touchPosition);
                // Comprueba si el toque está sobre una parte del modelo 3D
                isOver3DPart = isTapOver3DPart(touchPosition);
                Debug.Log(isOver3DPart);
            }

            if (touchOne.phase == TouchPhase.Moved)
            {
                if (aRRaycastManager.Raycast(touchOne.position, hits, TrackableType.Planes))
                {
                    Pose hitPose = hits[0].pose;
                    // Si no se encuentra sobre la interfaz de usuario y está sobre el modelo 3D,
                    // actualiza la posición del objeto
                    if (!isOverUI && isOver3DModel)
                    {
                        transform.position = hitPose.position;
                    }
                }
            }

            // Si hay dos toques en la pantalla
            if (Input.touchCount == 2)
            {
                Touch touchTwo = Input.GetTouch(1);
                if (touchOne.phase == TouchPhase.Began || touchTwo.phase == TouchPhase.Began)
                {
                    touchPositionDiff = touchTwo.position - touchOne.position;
                }

                if (touchOne.phase == TouchPhase.Moved || touchTwo.phase == TouchPhase.Moved)
                {
                    Vector2 currentTouchPosDiff = touchTwo.position - touchOne.position;
                    // Calcula el ángulo de rotación entre los dos toques y rota el modelo 3D en consecuencia
                    float angle = Vector2.SignedAngle(touchPositionDiff, currentTouchPosDiff);
                    item3DModel.transform.rotation = Quaternion.Euler(0, item3DModel.transform.rotation.eulerAngles.y - angle, 0);
                    touchPositionDiff = currentTouchPosDiff;
                }
            }

            // Si está sobre el modelo 3D, no hay un objeto 3D seleccionado y no está sobre la interfaz de usuario,
            // establece el modelo 3D seleccionado y lo coloca en el ARPointer
            if (isOver3DModel && item3DModel == null && !isOverUI)
            {
                GameManager.instance.ARPosition();
                item3DModel = itemSelected;
                itemSelected = null;
                aRPointer.SetActive(true);
                transform.position = item3DModel.transform.position;
                item3DModel.transform.parent = aRPointer.transform;
            }
        }
    }

    // Comprueba si el toque está sobre la interfaz de usuario
    private bool isTapOverUI(Vector2 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touchPosition.x, touchPosition.y);
        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, result);

        return result.Count > 0;
    }

    // Comprueba si el toque está sobre el modelo 3D
    private bool isTapOver3DModel(Vector2 touchPosition)
    {
        Ray ray = aRCamera.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit3DModel))
        {
            if (hit3DModel.collider.CompareTag("Item"))
            {
                itemSelected = hit3DModel.transform.gameObject;

                return true;
            }
        }
        return false;
    }

    // Comprueba si el toque está sobre una parte del modelo 3D
    private bool isTapOver3DPart(Vector2 touchPosition)
    {
        Ray ray = aRCamera.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit3DPart))
        {
            if (hit3DPart.collider.CompareTag("inferiorComplejo") || hit3DPart.collider.CompareTag("inferiorSimple") ||
                hit3DPart.collider.CompareTag("superiorComplejo") || hit3DPart.collider.CompareTag("superiorSimple"))
            {
                partSelected = hit3DPart.transform.gameObject;
                Debug.Log("Parte antes: " + partSelected.tag + " Con escala de: " + partSelected.transform.localScale);
                partSelected.transform.localScale = Vector3.zero;
                Debug.Log("Parte despues: " + partSelected.tag + " Con escala de: " + partSelected.transform.localScale);
                return true;
            }
        }
        return false;
    }

    // Establece la posición inicial del objeto en el menú principal
    private void SetItemPosition()
    {
        if (item3DModel != null)
        {
            item3DModel.transform.parent = null;
            aRPointer.SetActive(false);
            item3DModel = null;
        }
    }

    // Elimina el objeto 3D
    public void DeleteItem()
    {
        Destroy(item3DModel);
        aRPointer.SetActive(false);
        GameManager.instance.MainMenu();
    }
}
