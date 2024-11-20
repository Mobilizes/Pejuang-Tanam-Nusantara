using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public abstract class GameEntity : MonoBehaviour
{
    private int _hp = 100;

    public int Hp
    {
        get => _hp;
        protected set => _hp = math.max(_hp, 0);
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

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected int GetLane()
    {
        string rowName = transform.parent.parent.name;
        return int.Parse(rowName[(rowName.LastIndexOf('w') + 1)..]);
    }
}
