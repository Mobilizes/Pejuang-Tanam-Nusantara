class Peashooter : AttackerPlant
{
    public Peashooter(): base(100, 2)
    {
        Cost = 100;
        Cooldown = 5;

        Atk = 20;
    }
}