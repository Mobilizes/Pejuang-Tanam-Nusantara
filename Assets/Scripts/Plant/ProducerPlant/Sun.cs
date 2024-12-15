using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sun : MonoBehaviour
{

    private Vector2 _dropDirection;
    private Vector3 _newScale;
    [SerializeField]
    private float _dropTime;
    private float _dropSpeed;
    private float _groundY;
    [SerializeField]
    private uint _value;

    public uint Value
    {
        get => _value;
        set
        {
            _value = value;
            UpdateScale();
        }
    }

    public Sun(uint value)
    {
        Value = value;
    }

    public void Awake()
    {
        _dropDirection = new Vector2(Random.Range(-0.5f, 0.5f), -1).normalized;
        _newScale = transform.localScale;

        _groundY = transform.localPosition.y - 30;
        _dropSpeed = math.abs((transform.localPosition.y - _groundY) * _dropDirection.y) / _dropTime ;
    }

    public void Update()
    {
        if (transform.localPosition.y > _groundY)
        {
            transform.Translate(_dropSpeed * Time.deltaTime * _dropDirection);
        }

        if (_value != _newScale.x * 25 / 2)
        {
            UpdateScale();
        }

        transform.localScale = _newScale;
    }

    public void UpdateScale()
    {
        float scale = _value * 2 / 25;
        _newScale = new Vector3(scale, scale, 1);
    }
}
