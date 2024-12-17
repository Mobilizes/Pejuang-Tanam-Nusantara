using UnityEngine;

[System.Serializable]
public class Spawning_Zombies
{
    public float SpawnTime;       // Total time to spawn zombies (stop spawning after this time)
    public float spawnInterval;   // Interval between spawns
    public int Spawner;           // Specific spawner index
    public bool RandomSpawn;      // Randomize spawn location
    public int zombieType;        // Type of zombie
    public bool isDone;           // Flag for stopping spawning
    [HideInInspector]
    public float lastSpawnTime;   // Time of the last spawn
    [HideInInspector]
    public float nextSpawnTime;

}

public enum ZombieType
{
    BasicZombie,
    ConeZombie,
    BucketZombie
}