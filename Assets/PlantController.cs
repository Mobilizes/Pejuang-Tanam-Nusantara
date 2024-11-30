using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class PlantController : MonoBehaviour
{
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

        if (toAttack != null)
        {
            if (attackTime <= Time.time)
            {
                Instantiate(bullet, transform);
                attackTime = Time.time + attackCooldown;
            }
        }
    }

}
