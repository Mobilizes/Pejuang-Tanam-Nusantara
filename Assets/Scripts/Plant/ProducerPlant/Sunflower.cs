public class Sunflower : ProducerPlant
{
    public Sunflower() : base(100)
    {
        Cost = 50;
        Cooldown = 5;
        Interval = 20;
    }
}