using UnityEngine;

public class Wallnut : DefensePlants
{
    public Wallnut() : base(100, false)
    {
        Dmg = 0;
        Cooldown = 5;
        Cost = 50;
    }
}