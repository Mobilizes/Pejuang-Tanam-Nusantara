using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

using Random = UnityEngine.Random;

public class Sun : MonoBehaviour, IPointerDownHandler
{
    private GameManager _gameManager;

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

    private void Start()
    {
        _gameManager = GameManager.instance;
    }

    public void Awake()
    {
        if (_dropTime == 0) _dropTime = 0.5f;

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

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left) return;

        Debug.Log("clicked");
        _gameManager.sunPoints += Value;
        Destroy(gameObject);
    }
}
