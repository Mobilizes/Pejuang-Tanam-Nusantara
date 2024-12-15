using UnityEngine;
using Assets.Scripts.Zombie;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour
{
    public List<GameObject> zombiePrefabs;
    public List<Spawning_Zombies> zombies;

    private void Update()
    {
        foreach (Spawning_Zombies zombie in zombies)
        {
            if (zombie.isSpawned == false && zombie.SpawnTime <= Time.time)
            {
                if (zombie.RandomSpawn)
                {
                    zombie.Spawner = Random.Range(0, transform.childCount);
                }

                GameObject zombieInstance = Instantiate(zombiePrefabs[(int)zombie.zombieType], transform.GetChild(zombie.Spawner).transform);
                transform.GetChild(zombie.Spawner).GetComponent<SpawnPoint>().zombies.Add(zombieInstance);
                zombie.isSpawned = true;
            }
        }
    }
}
