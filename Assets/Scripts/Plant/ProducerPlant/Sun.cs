using System;
using Unity.Mathematics;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.UI;
using Random = UnityEngine.Random;

public class Sun : MonoBehaviour
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

        _groundY = transform.position.y - 30;
        _dropSpeed = math.abs((transform.position.y - _groundY) * _dropDirection.y) / _dropTime;
    }

    public void Update()
    {
        if (transform.position.y > _groundY)
        {
            transform.Translate(_dropSpeed * Time.deltaTime * _dropDirection);
        }

        if (Input.mousePosition.x > 0 && Input.mousePosition.y > 0 &&
            Input.mousePosition.x < Screen.width &&
            Input.mousePosition.y < Screen.height)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if (mousePosition.x > transform.position.x - 32 && mousePosition.x < transform.position.x + 32 &&
                mousePosition.y > transform.position.y - 32 && mousePosition.y < transform.position.y + 32 &&
                Input.GetMouseButtonDown(0))
            {
                Debug.Log("clicked");
                _gameManager.sunPoints += Value;
                Destroy(gameObject);
            }
        }
    }
}
