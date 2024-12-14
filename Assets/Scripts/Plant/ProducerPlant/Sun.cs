using System;
using Unity.Mathematics;
using UnityEngine;

using Random = UnityEngine.Random;

public class Sun : MonoBehaviour
{
    private const float _dropSpeed = 18;

    private Vector2 _dropDirection;
    private Vector3 _newScale;
    private uint _value;

    public uint Value
    {
        get => _value;
        set
        {
            _value = value;
            _newScale = new Vector3(value / 25, value / 25, 1);
        }
    }

    public Sun(uint value)
    {
        Value = value;
    }

    public void Awake()
    {
        _dropDirection = new Vector2(Random.Range(-1f, 1f), -1).normalized;
        _newScale = transform.localScale;
    }

    public void Update()
    {
        if (transform.localPosition.y > GetGroundY())
        {
            transform.Translate(_dropSpeed * Time.deltaTime * _dropDirection);
        }

        if (transform.localScale != _newScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _newScale, Time.deltaTime);
        }
    }

    private float GetGroundY()  // Maybe will add calculation for dynamic ground height based on plant
    {
        return -30;
    }
}
