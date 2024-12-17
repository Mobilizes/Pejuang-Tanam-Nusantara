using UnityEngine;
using Assets.Scripts.Zombie;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour
{
    public List<GameObject> zombiePrefabs;
    public List<Spawning_Zombies> zombies;
    public float maxSpawnTime = 60f;
    private float spawnStartTime;

    private void Start()
    {
        spawnStartTime = Time.time;
        foreach (Spawning_Zombies zombie in zombies)
        {
            zombie.lastSpawnTime = Time.time;
            zombie.nextSpawnTime = Time.time + zombie.spawnInterval; 
            zombie.isDone = false; 
        }
    }

    private void Update()
    {
        if (Time.time >= spawnStartTime + maxSpawnTime)
        {
            return;
        }

        foreach (Spawning_Zombies zombie in zombies)
        {
            if (zombie.isDone) continue; 
            

            if (Time.time >= zombie.nextSpawnTime)
            {
                SpawnZombie(zombie);
                zombie.lastSpawnTime = Time.time;
                zombie.nextSpawnTime = Time.time + zombie.spawnInterval;

                if (Time.time >= zombie.SpawnTime)
                {
                    zombie.isDone = true;
                }
            }
        }
    }

    private void SpawnZombie(Spawning_Zombies zombie)
    {
        GameObject zombieInstance = Instantiate(zombiePrefabs[(int)zombie.zombieType], transform.GetChild(zombie.Spawner).transform);
        transform.GetChild(zombie.Spawner).GetComponent<SpawnPoint>().zombies.Add(zombieInstance);
    }
}
