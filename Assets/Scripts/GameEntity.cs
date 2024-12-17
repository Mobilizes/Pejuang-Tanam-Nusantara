using Unity.Mathematics;
using UnityEditor.Compilation;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    [SerializeField]
    private int _maxHp;
    private int _hp;
    private bool _hasArmor;
    public int MaxHp
    {
        get => _maxHp;
        protected set => _maxHp = math.max(value, 0);
    }

    public int Hp
    {
        get => _hp;
        protected set => _hp = math.clamp(value, 0, MaxHp);
    }

    public bool hasArmor
    {
        get => _hasArmor;
        protected set => _hasArmor = value;
    }
    protected GameEntity(int maxHp, bool hasArmor)
    {
        MaxHp = maxHp;

        Hp = MaxHp;

        this.hasArmor = hasArmor;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            throw new System.ArgumentException("Damage cannot be negative");
        }
        if (hasArmor)
        {
            TakeArmorDamage(damage);
        }
        else
        {
            Hp -= damage;
            if (Hp == 0)
            {
                Die();
            }
        }
        
    }

    public bool IsDead()
    {
        return Hp == 0;
    }

    protected virtual void TakeArmorDamage(int damage)
    {
        Hp -= damage;
        if (Hp == 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected int GetLane()
    {
        Transform currentParent = transform.parent != null ? transform.parent : throw new System.Exception("GameEntity node must be a child node of a row container node!");

        while (currentParent.parent != null)
        {
            if (currentParent.name.StartsWith("Row"))
            {
                return int.Parse(currentParent.name[3..]);
            }

            currentParent = currentParent.parent;
        }

        throw new System.Exception("GameEntity node must be a child node of a row container!");
    }
}
