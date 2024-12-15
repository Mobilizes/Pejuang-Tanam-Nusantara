using System.Collections;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public int Health;
    public float movementSpeed;
    private bool isStopped;
    public int DamageValue;
    public float DamageCooldown;
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
            StartCoroutine(Attack(collision));
            isStopped = true;
        }
    }
    IEnumerator Attack(UnityEngine.Collider2D collision)
    {
        if (collision == null)
        {
            isStopped = false;
        }
        else
        { 
            collision.gameObject.GetComponent<PlantController>().ReceiveDamage(DamageValue);
            yield return new WaitForSeconds(DamageCooldown);
            StartCoroutine(Attack(collision));
        }
    }
    public void ReceiveDamage(int damage)
    {
        if (Health - damage <= 0)
        {
            transform.parent.GetComponent<SpawnPoint>().zombies.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            Health -= damage;
        }
    }
}