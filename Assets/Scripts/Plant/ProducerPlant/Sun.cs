using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sun : MonoBehaviour
{
    private Vector2 _dropDirection;
    [SerializeField]
    private float _dropTime;
    private float _dropSpeed;
    private float _groundY;
    [SerializeField]
    private uint _value;

    public uint Value
    {
        get => _value;
        set => _value = value;
    }

    public Sun(uint value)
    {
        Value = value;
    }

    public void Awake()
    {
        _dropDirection = new Vector2(Random.Range(-0.5f, 0.5f), -1).normalized;

        _groundY = transform.localPosition.y - 30;
        _dropSpeed = math.abs((transform.localPosition.y - _groundY) * _dropDirection.y) / _dropTime;
    }

    public void Update()
    {
        if (transform.localPosition.y > _groundY)
        {
            transform.Translate(_dropSpeed * Time.deltaTime * _dropDirection);
        }
    }
}
