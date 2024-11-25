using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Zombie
{
    public abstract class Zombie : GameEntity
    {
        protected Animator animator;
        protected float _speed = 1;
        private float _timer;

        protected float Interval { get; set; } = 1;
        protected float Speed
        {
            get => _speed;
            set => _speed = math.max(value, 0);
        }
        private float Timer
        {
            get => _timer;
            set => _timer = math.min(value, Interval);
        }

        protected bool Attacking { get; set; } = false;
        protected int Atk { get; set; } = 20;

        protected Zombie()
        {
            _timer = Interval;
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Plant"))
            {
                Debug.Log($"Zombie collided with a plant at lane " + GetLane());

                Attacking = true;

                //GameEntity plant = other.gameObject.GetComponent<GameEntity>();
                //if (plant is IAttackable)
                //{
                //    _attacking = true;
                //    Attack(plant);
                //}
            }
        }

        protected virtual void OnCollisionExit2D(Collision2D other)
        {
            Attacking = false;
        }

        protected virtual void Start()
        {
            animator = GetComponent<Animator>();
        }

        protected virtual void Update()
        {
            _timer += Time.deltaTime;

            if (!Attacking)
            {
                Move();
            }

            animator.SetBool("Attacking", Attacking);
            if (Hp < MaxHp / 2) animator.SetBool("Weak", true);

            if (Hp == 0) { Die(); }
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
            Debug.Log("Zombie died");

            animator.SetBool("Dead", true);

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 5)
            {
                base.Die();
            }
        }

        protected void Move()
        {
            transform.position += Speed * Time.deltaTime * 0.2f * Vector3.left;
        }
    }
}
