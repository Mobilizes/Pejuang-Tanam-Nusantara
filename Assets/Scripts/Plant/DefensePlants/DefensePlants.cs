using UnityEngine;
using Unity.Mathematics;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.EventSystems.EventTrigger;

public class DefensePlants : Plant
{
    private int _dmg;
    private bool _dead;
    protected GameObject target;

    public bool isExplosive { get; private set; }
    
    public int Dmg
    {
        get => _dmg;
        protected set => _dmg = math.max(value, 0);
    }

    public DefensePlants(int maxHp, bool isExplosive) : base(maxHp)
    {
        this.isExplosive = isExplosive;
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            target = other.gameObject;
            if (this.isExplosive)
            { 
                Explode(target.GetComponent<GameEntity>());
            }
        }
    }
    public void Explode(GameEntity entity)
    {
        Debug.Log(gameObject.name + " exploded with damage: " + Dmg);
        entity.TakeDamage(Dmg);
        Destroy(this.gameObject);
    }
}