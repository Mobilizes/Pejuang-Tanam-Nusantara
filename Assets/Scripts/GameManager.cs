using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject draggingObject;
    public GameObject currentContainer;
    public uint sunPoints = 100;

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
            objectGame.GetComponent<AttackerPlant>().zombies = currentContainer.GetComponent<ObjectContainer>().spawnPoint.zombies;
            currentContainer.GetComponent<ObjectContainer>().isFull = true;
        }
    }   
}
