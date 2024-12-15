using UnityEngine;
using Unity.Mathematics;
using System.Collections.Generic;

public class AttackerPlant : Plant
{
    public GameObject bullet;
    public List<GameObject> zombies;
    public SpawnPoint zombiesSpawnPoint;

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

    public void Awake()
    {
        GameObject spawner = GameObject.Find("Spawner");

        foreach (Transform child in spawner.GetComponentInChildren<Transform>())
        {
            zombiesSpawnPoint = child.GetComponent<SpawnPoint>();

            if (zombiesSpawnPoint.row == GetLane()) break;
        }
    }

    public void Update()
    {
        if ((zombiesSpawnPoint.zombies.Count > 0 || zombies.Count > 0) && _isAttacking == false)
        {
            _isAttacking = true;
        }
        else if ((zombiesSpawnPoint.zombies.Count == 0 || zombies.Count == 0) && _isAttacking == true)
        {
            _isAttacking = false;
        }

        if (_isAttacking)
        {
            
            if (AttackTime <= Time.time)
            {
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
}
