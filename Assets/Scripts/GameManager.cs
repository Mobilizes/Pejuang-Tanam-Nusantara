using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject draggingObject;
    public GameObject currentContainer;

    public static GameManager instance;

    public void Awake()
    {
        instance = this;
    }
    public void PlaceObject()
    {
        if (draggingObject != null && currentContainer != null)
        {
            Instantiate(draggingObject.GetComponent<ObjectDragging>().card.object_Game, currentContainer.transform);
            currentContainer.GetComponent<ObjectContainer>().isFull = true;
        }
    }   
}