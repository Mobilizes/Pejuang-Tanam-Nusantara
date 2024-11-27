using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Zombie
{
    public class Zombie : GameEntity
    {
        protected Animator animator;
        protected GameObject target;

        protected float _speed = 1;
        private int _atk = 20;
        private float _interval = 1;
        private float _timer;

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

        protected Zombie() : base()
        {
            Timer = Interval;
        }

        protected void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Plant"))
            {
                Debug.Log($"Zombie collided with a plant at lane " + GetLane());

                Attacking = true;
                target = other.gameObject;
            }
        }

        protected void OnCollisionExit2D(Collision2D _)
        {
            Attacking = false;
        }

        protected void Start()
        {
            animator = GetComponent<Animator>();
        }

        protected void Update()
        {
            animator.SetBool("Attacking", Attacking);
            animator.SetBool("Weak", Hp < MaxHp / 2);
            if (Hp == 0)
            {
                Die();
                return;
            }

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

        protected new void Die()
        {
            animator.SetBool("Dead", true);

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 5)
            {
                base.Die();
            }
        }

        protected void Move()
        {
            transform.position += Speed * Time.deltaTime * 0.2f * transform.localScale.x * Vector3.left;
        }
    }
}
