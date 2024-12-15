using Unity.Mathematics;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    [SerializeField]
    private int _maxHp;
    private int _hp;

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

    protected GameEntity(int maxHp)
    {
        MaxHp = maxHp;

        Hp = MaxHp;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            throw new System.ArgumentException("Damage cannot be negative");
        }

        Hp -= damage;
        if (Hp == 0)
        {
            Die();
        }
    }

    public bool IsDead()
    {
        return Hp == 0;
    }

    protected void Die()
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
