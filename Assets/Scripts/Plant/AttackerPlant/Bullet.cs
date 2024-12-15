using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float movementSpeed;
    public int DamageValue;
    private void Update()
    {
        transform.Translate(new Vector3(movementSpeed, 0, 0) * Time.deltaTime);

        if (transform.position.x > Screen.width)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameEntity gameEntity = collision.gameObject.GetComponent<GameEntity>();

        if (collision.gameObject.layer == 11 && !gameEntity.IsDead())
        {
            gameEntity.TakeDamage(DamageValue);
            Destroy(this.gameObject);
        }
    }
}
