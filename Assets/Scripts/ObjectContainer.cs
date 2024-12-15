using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ObjectContainer : MonoBehaviour
{
    public bool isFull;
    public GameManager gameManager;
    public Image backgroundImage;
    public SpawnPoint spawnPoint;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie")) return; 

        if (gameManager.draggingObject != null && !isFull && gameManager.currentContainer == null)
        {
            gameManager.currentContainer = this.gameObject;
            backgroundImage.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie")) return; 

        if (gameManager.currentContainer == this.gameObject)
        {
            gameManager.currentContainer = null;
            backgroundImage.enabled = false;
        }
    }

    private void Update()
    {
        isFull = transform.childCount > 0;
    }
}
