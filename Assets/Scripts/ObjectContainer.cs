using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ObjectContainer : MonoBehaviour
{
    public bool isFull;
    public GameManager gameManager;
    public Image backgroundImage;
    public void Start()
    {
        gameManager = GameManager.instance;
    }
    public void OnTriggerStay2D(Collider2D collision)
    { 
        if (gameManager.draggingObject != null && isFull == false && gameManager.currentContainer == null)
        {
            gameManager.currentContainer = this.gameObject;
            backgroundImage.enabled = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(gameManager.currentContainer == this.gameObject) 
            gameManager.currentContainer = null;
        backgroundImage.enabled = false;
    }
}
