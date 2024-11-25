using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public abstract class GameEntity : MonoBehaviour
{
    protected Animator _animator;
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
        protected set => _hp = math.max(value, 0);
    }

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();

        Hp = MaxHp;
    }

    public virtual void TakeDamage(int damage)
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
