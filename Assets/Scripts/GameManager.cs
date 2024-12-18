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
        Plant plant = draggingObject.GetComponent<ObjectDragging>().card.object_Game.GetComponent<Plant>();

        if (draggingObject != null && currentContainer != null && sunPoints >= plant.Cost)
        {
            sunPoints -= plant.Cost;

            GameObject objectGame = Instantiate(draggingObject.GetComponent<ObjectDragging>().card.object_Game, currentContainer.transform);
            currentContainer.GetComponent<ObjectContainer>().isFull = true;
        }
    }   
}
