using UnityEngine;

public class CherryBomb : DefensePlants
{
    public CherryBomb() : base(100, true)
    {
        Dmg = 500;
        Cost = 100;
        Cooldown = 10;
    }
}
