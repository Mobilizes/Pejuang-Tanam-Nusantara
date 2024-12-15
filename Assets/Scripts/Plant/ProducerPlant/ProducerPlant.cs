using Unity.Mathematics;
using UnityEngine;

public class ProducerPlant : Plant
{
    public Sun sun;
    protected Animator animator;

    private float _interval;
    private float _intervalTimer;

    public float Interval
    {
        get => _interval;
        protected set => _interval = math.max(value, 0);
    }
    public float IntervalTimer
    {
        get => _intervalTimer;
        protected set => _intervalTimer = math.clamp(value, 0, Interval);
    }

    protected ProducerPlant(int maxHp) : base(maxHp)
    {
        IntervalTimer = Interval;
    }

    protected void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        if (IntervalTimer <= 0)
        {
            Produce();
            IntervalTimer = Interval;
        }
        else
        {
            IntervalTimer -= Time.deltaTime;
        }
    }

    protected void Produce()
    {
        if (sun == null)
        {
            Debug.LogError("Sun prefab is not set for " + gameObject.name);
            return;
        }

        Sun newSun = Instantiate(sun, transform.position, quaternion.identity);
        newSun.transform.SetParent(transform.parent.parent.parent);
        newSun.Value = 25;
    }
}
