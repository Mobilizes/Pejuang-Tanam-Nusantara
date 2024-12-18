using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class SunSpawner : MonoBehaviour
{
    public Sun sun;

    [SerializeField]
    private float _interval;
    private float _timer;

    public float Interval
    {
        get => _interval;
        set => _interval = value;
    }

    public float Timer
    {
        get => _timer;
        set => _timer = math.clamp(value, 0, Interval);
    }

    public void Update()
    {
        Timer += Time.deltaTime;

        if (Timer == Interval)
        {
            SpawnSun();

            Timer = 0;
        }
    }

    private void SpawnSun()
    {
        float randomX = Random.Range(200, Screen.width - 200);
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(randomX, Screen.height, Camera.main.nearClipPlane));
        spawnPosition.z = 0;

        Sun newSun = Instantiate(sun, spawnPosition, quaternion.identity);
        newSun.transform.SetParent(GameObject.Find("World Canvas").transform);

        newSun.Value = 25;
        newSun.GroundY = Camera.main.ScreenToWorldPoint(new Vector3(randomX, Random.Range(200, Screen.height - 100), Camera.main.nearClipPlane)).y;
        newSun.DropTime = 5f;
    }
}
