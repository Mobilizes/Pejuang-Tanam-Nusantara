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
            GameObject objectGame = Instantiate(draggingObject.GetComponent<ObjectDragging>().card.object_Game, currentContainer.transform);
            objectGame.GetComponent<PlantController>().zombies = currentContainer.GetComponent<ObjectContainer>().spawnPoint.zombies;
            currentContainer.GetComponent<ObjectContainer>().isFull = true;
        }
    }   
}
