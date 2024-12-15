using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Zombie
{
    public class Zombie : GameEntity
    {
        private const float SPEED_MULTIPLIER = 5.0f;

        protected Animator animator;
        protected GameObject target;

        protected float _speed = 1;
        private int _atk = 20;
        private float _interval = 1;
        private float _timer;
        private float _deathTime;

        protected float Interval
        {
            get => _interval;
            set => _interval = math.max(value, 0);
        }
        protected float Speed
        {
            get => _speed;
            set => _speed = math.max(value, 0);
        }
        protected float Timer
        {
            get => _timer;
            private set => _timer = math.clamp(value, 0, Interval);
        }

        protected bool Attacking { get; set; } = false;
        protected int Atk
        {
            get => _atk;
            set => _atk = math.max(value, 0);
        }

        protected Zombie(int maxHp) : base(maxHp)
        {
            Timer = Interval;
        }

        protected void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Plant"))
            {
                Attacking = true;
                target = other.gameObject;
            }
        }

        protected void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject == target || target == null || target.GetComponent<GameEntity>().IsDead())
            {
                Attacking = false;
                target = null;
            }
        }

        protected void Start()
        {
            animator = GetComponent<Animator>();
        }

        protected void Update()
        {
            if (Hp == 0)
            {
                Die();
                return;
            }

            animator.SetBool("Attacking", Attacking);
            animator.SetBool("Weak", Hp < MaxHp / 2);

            if (Attacking)
            {
                Attack(target.GetComponent<GameEntity>());
            }
            else
            {
                Move();
            }

            Timer += Time.deltaTime;
        }

        protected void Attack(GameEntity entity)
        {
            if (Timer == Interval)
            {
                entity.TakeDamage(Atk);
                Timer = 0;
            }
        }

        protected override void Die()
        {
            animator.SetBool("Dead", true);
            _deathTime += Time.deltaTime;

            transform.parent.GetComponent<SpawnPoint>().zombies.Remove(gameObject);
            transform.GetComponent<Collider2D>().enabled = false;

            if (_deathTime > 7)
            {
                base.Die();
            }
        }

        protected void Move()
        {
            transform.position += SPEED_MULTIPLIER * Speed *
                                  Time.deltaTime * Vector3.left;
        }
    }
}
