using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Zombie
{
    public class Zombie : GameEntity, ISlowable
    {
        private const float SPEED_MULTIPLIER = 5.0f;
        private const float SLOW_MULTIPLIER = 0.5f;
        private const float SLOW_DURATION = 10.0f;

        protected Animator animator;
        protected GameObject target;

        private float _interval = 1;
        private float _speed = 1;
        private float _timer;
        private float _slowTimer;
        private float _deathTime;
        private int _atk = 20;
        private int _armor;

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
        protected float SlowTimer
        {
            get => _slowTimer;
            private set => _slowTimer = math.clamp(value, 0, SLOW_DURATION);
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
        protected bool IsSlowed { get; set; }

        protected int Armor
        {
            get => _armor;
            set => _armor = math.max(value, 0);
        }

        protected Zombie(int maxHp) : base(maxHp, true)
        {
            Timer = Interval;
            SlowTimer = 0;
            IsSlowed = false;
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

            if (Attacking)
            {
                animator.Play("Eat");
            }

            if (IsLow())
            {
                animator.Play("Walk_Weak");
            }

            if (IsLow() && Attacking)
            {
                animator.Play("Eat_Weak");
            }

            animator.SetBool("Attacking", Attacking);
            animator.SetBool("Weak", IsLow());

            if (Attacking)
            {
                Attack(target.GetComponent<GameEntity>());
            }
            else
            {
                Move();
            }

            Timer += Time.deltaTime;
            SlowTimer -= Time.deltaTime;

            if (SlowTimer == 0)
            {
                Unslow();
            }
        }

        public void Slow()
        {
            if (!IsSlowed)
            {
                Speed *= SLOW_MULTIPLIER;
            }

            SlowTimer = SLOW_DURATION;
            IsSlowed = true;

            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }

        public void Unslow()
        {
            if (IsSlowed)
            {
                Speed *= 1 / SLOW_MULTIPLIER;
            }

            SlowTimer = 0;
            IsSlowed = false;

            gameObject.GetComponent<Renderer>().material.color = Color.white;
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
            if (!animator.GetBool("Dead"))
            {
                transform.position += Vector3.left * 30;
            }

            animator.Play("Dead");
            animator.SetBool("Dead", true);
            _deathTime += Time.deltaTime;

            transform.parent.GetComponent<SpawnPoint>().zombies.Remove(gameObject);
            transform.GetComponent<Collider2D>().enabled = false;

            if (_deathTime > 7)
            {
                base.Die();
            }
        }

        protected override void TakeArmorDamage(int damage)
        {

            if (this._armor < 0)
            {
                Debug.Log("Armor hancur");
                base.hasArmor = false;
            }
            else this._armor -= damage;
        }
        protected void Move()
        {
            transform.position += SPEED_MULTIPLIER * Speed *
                                  Time.deltaTime * Vector3.left;
        }

        protected new int GetLane()
        {
            return transform.parent.GetComponent<SpawnPoint>().row;
        }

        protected bool IsLow()
        {
            return Hp <= MaxHp / 2;
        }
    }
}
