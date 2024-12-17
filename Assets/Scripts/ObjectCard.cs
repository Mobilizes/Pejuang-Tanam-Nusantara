using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ObjectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject object_Drag;
    public GameObject object_Game;
    public Canvas canvas;
    private GameObject objectDragInstance;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (objectDragInstance == null) return;

        if (eventData.button != PointerEventData.InputButton.Left) return;

        objectDragInstance.transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        if (gameManager.sunPoints < object_Game.GetComponent<Plant>().Cost) return;

        objectDragInstance = Instantiate(object_Drag, canvas.transform);
        objectDragInstance.transform.position = Input.mousePosition;
        objectDragInstance.GetComponent<ObjectDragging>().card = this;

        gameManager.draggingObject = objectDragInstance;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (gameManager.draggingObject == null) return;

        if (eventData.button != PointerEventData.InputButton.Left) return;

        if (gameManager.sunPoints < object_Game.GetComponent<Plant>().Cost)
        {
            Destroy(objectDragInstance);
            gameManager.draggingObject = null;
            return;
        }

        gameManager.PlaceObject();
        gameManager.draggingObject = null;
        Destroy(objectDragInstance);
    }
}