using UnityEngine;

[System.Serializable]
public class Spawning_Zombies
{
    public int SpawnTime;
    public ZombieType zombieType;
    public int Spawner;
    public bool RandomSpawn;
    public bool isSpawned;
}

public enum ZombieType
{
    BasicZombie,
    ConeZombie,
    BucketZombie
}