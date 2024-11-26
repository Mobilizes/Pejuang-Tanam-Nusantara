using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Zombie
{
    public abstract class Zombie : GameEntity
    {
        protected float _speed = 1;
        private float _timer;
        private GameEntity _target;

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

        protected Zombie() : base()
        {
            _timer = Interval;
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Plant"))
            {
                _target = other.gameObject.GetComponent<GameEntity>();
                Attacking = true;

                Debug.Log($"Zombie collided with a plant at lane " + GetLane());
            }
        }

        protected virtual void OnCollisionExit2D(Collision2D other)
        {
            _target = null;
            Attacking = false;
        }

<<<<<<< Updated upstream
=======
        protected virtual void Start()
        {
            Hp = MaxHp;
            animator = GetComponent<Animator>();
        }

>>>>>>> Stashed changes
        protected virtual void Update()
        {
            _timer += Time.deltaTime;

            if (!Attacking)
            {
                Move();
            }

            _animator.SetBool("Attacking", Attacking);
            if (Hp < MaxHp / 2) _animator.SetBool("Weak", true);
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

            base.Die();
        }

        protected void Move()
        {
            transform.position += Speed * Time.deltaTime * 0.2f * Vector3.left;
        }
    }
}
