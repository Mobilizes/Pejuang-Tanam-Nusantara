using UnityEngine;
using Unity.Mathematics;
using System.Collections.Generic;

public class AttackerPlant : Plant
{
    public Animator animator;
    public GameObject bullet;
    public List<GameObject> zombies;

    private bool _isAttacking;
    [SerializeField]
    private float _attackInterval;
    private float _attackTime;
    [SerializeField]
    private int _atk;

    public float AttackInterval
    {
        get => _attackInterval;
        protected set => _attackInterval = math.max(value, 0);
    }

    public float AttackTime
    {
        get => _attackTime;
        protected set => _attackTime = math.max(value, 0);
    }

    public int Atk
    {
        get => _atk;
        protected set => _atk = math.max(value, 0);
    }

    public bool IsAttacking
    {
        get => _isAttacking;
        protected set => _isAttacking = value;
    }

    public AttackerPlant(int maxHp, int attackInterval) : base(maxHp)
    {
        AttackInterval = attackInterval;
        AttackTime = 0;
        IsAttacking = false;
    }

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (IsAZombieInLane() && IsAttacking == false)
        {
            IsAttacking = true;
        }
        else if ((!IsAZombieInLane()) && IsAttacking == true)
        {
            IsAttacking = false;
        }

        if (IsAttacking)
        {
            if (AttackTime <= Time.time)
            {
                animator.Play("Attacking");
                GameObject bulletInstance = Instantiate(bullet, transform);
                bulletInstance.GetComponent<Bullet>().DamageValue = Atk;
                bulletInstance.transform.localScale /= transform.localScale.x;
                AttackTime = Time.time + AttackInterval;
            }
        }
    }
    public void ReceiveDamage(int damage)
    {
        Debug.Log(gameObject.name + " received damage: " + damage);
        if (Hp - damage <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Hp -= damage;
        }
    }

    public bool IsAZombieInLane()
    {
        GameObject spawner = GameObject.Find("Spawner");

        foreach (Transform child in spawner.GetComponentInChildren<Transform>())
        {
            if (child.GetComponent<SpawnPoint>().row == GetLane())
            {
                if (child.GetComponent<SpawnPoint>().zombies.Count > 0) return true;
            }
        }

        return false;
    }
}
