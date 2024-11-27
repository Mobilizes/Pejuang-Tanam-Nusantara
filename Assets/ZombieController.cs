using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float movementSpeed;
    void Update()
    {
        transform.Translate(new Vector3(movementSpeed * -1, 0, 0));
    }
}