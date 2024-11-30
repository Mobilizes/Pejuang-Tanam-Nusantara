using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float movementSpeed;
    private bool isStopped;
    void Update()
    {
        if (!isStopped)
        {
            transform.Translate(new Vector3(movementSpeed * -1, 0, 0));
        }
        
    }
    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Debug.Log("tes");
        if (collision.gameObject.layer == 10)
        {
            Debug.Log("tes");
            isStopped = true;
        }
    }
}