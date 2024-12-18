using System;
using Unity.Mathematics;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.UI;
using Random = UnityEngine.Random;

public class Sun : MonoBehaviour, IPointerDownHandler
{
    public GameManager gameManager;

    private Vector2 _dropDirection;
    [SerializeField]
    private float _dropTime;
    private float _dropSpeed;
    private float _groundY;
    [SerializeField]
    private float _expiration;
    private float _lifetime;
    [SerializeField]
    private uint _value;

    public float DropTime
    {
        get => _dropTime;
        set
        {
            _dropTime = math.max(value, 0.5f);
            _dropSpeed = math.abs((transform.position.y - _groundY) * _dropDirection.y) / _dropTime;
        }
    }

    public float GroundY
    {
        get => _groundY;
        set => _groundY = value;
    }

    public float Expiration
    {
        get => _expiration;
        set => _expiration = value;
    }

    public uint Value
    {
        get => _value;
        set => _value = value;
    }

    private float Lifetime
    {
        get => _lifetime;
        set => _lifetime = math.clamp(value, 0, Expiration);
    }

    public Sun(uint value)
    {
        Value = value;
    }

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    public void Awake()
    {
        _dropDirection = new Vector2(Random.Range(-0.5f, 0.5f), -1).normalized;

        _groundY = transform.position.y - 30;
        _dropSpeed = math.abs((transform.position.y - _groundY) * _dropDirection.y) / _dropTime;
    }

    public void Update()
    {
        if (transform.position.y > _groundY)
        {
            transform.Translate(_dropSpeed * Time.deltaTime * _dropDirection);
        }

        Lifetime += Time.deltaTime;

        if (Lifetime == Expiration)
        {
            Destroy(gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        gameManager.sunPoints += Value;
        Destroy(gameObject);
    }
}
