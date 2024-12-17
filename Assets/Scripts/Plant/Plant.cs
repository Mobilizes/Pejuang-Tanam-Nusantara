using Unity.Mathematics;
using UnityEngine;

public class Plant : GameEntity
{
    private uint _cost;
    private float _cooldown;
    private float _cooldownTimer;

    public uint Cost
    {
        get => _cost;
        protected set => _cost = value;
    }
    public float Cooldown
    {
        get => _cooldown;
        protected set => _cooldown = math.max(value, 0);
    }
    public float CooldownTimer
    {
        get => _cooldownTimer;
        protected set => _cooldownTimer = math.clamp(value, 0, Cooldown);
    }

    protected Plant(int maxHp) : base(maxHp, false)
    {
        CooldownTimer = Cooldown;
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
