using UnityEngine;
using Assets.Scripts.Zombie;
using NUnit.Framework;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour
{
    public List<GameObject> zombiePrefabs;
    public List<Spawning_Zombies> zombies;

    private void Update()
    {
        foreach (Spawning_Zombies zombie in zombies)
        {
            if (zombie.isSpawnned == false && zombie.SpawnTime <= Time.time)
            {
                GameObject zombieInstance = Instantiate(zombiePrefabs[(int) zombie.zombieType], transform.GetChild(zombie.Spawner).transform);
                zombie.isSpawnned = true;
                
            }
        }
    }
}

