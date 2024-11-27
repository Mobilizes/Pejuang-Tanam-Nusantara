using UnityEngine;

[System.Serializable]
public class Spawning_Zombies
{
    public int SpawnTime;
    public ZombieType zombieType;
    public int Spawner;
    public bool RandomSpawn;
    public bool isSpawnned;
}

public enum ZombieType
{
    Zombie_Normal,
    Zombie_Cone,
    Zombie_Bucket
}