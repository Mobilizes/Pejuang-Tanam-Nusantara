using UnityEngine;
using UnityEngine.UI;

public class SunCounter : MonoBehaviour
{
    public GameManager gameManager;

    public void Start()
    {
        gameManager = GameManager.instance;
    }

    public void Update()
    {
        gameObject.GetComponentInChildren<Text>().text = gameManager.sunPoints.ToString();
    }
}
