using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float movementSpeed;
    public int DamageValue;
    private void Update()
    {
        transform.Translate(new Vector3(movementSpeed, 0, 0));
    }
    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {

        if (collision.gameObject.layer == 11)
        {
            collision.gameObject.GetComponent<ZombieController>().ReceiveDamage(DamageValue);
            Destroy(this.gameObject);
        }
    }
}