using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
public class PlantController : MonoBehaviour
{
    public int Health;
    public GameObject bullet;
    public List<GameObject> zombies;
    public GameObject toAttack;
    public float attackCooldown;
    public float attackTime;
    public int DamageValue;
    public bool isAttacking;


    public void Update()
    {
        if (zombies.Count > 0 && isAttacking == false)
        {
            isAttacking = true;
        }
        else if (zombies.Count == 0 && isAttacking == true)
        {
            isAttacking = false;
        }

        if (isAttacking)
        {
            
            if (attackTime <= Time.time)
            {
                GameObject bulletInstance = Instantiate(bullet, transform);
                bulletInstance.GetComponent<Bullet>().DamageValue = DamageValue;
                attackTime = Time.time + attackCooldown;
            }
        }
    }
    public void ReceiveDamage(int damage)
    {
        if (Health - damage <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Health -= damage;
        }
    }
}
