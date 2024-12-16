using UnityEngine;

class SnowBullet : Bullet
{
    public new void OnTriggerEnter2D(Collider2D collision)
    {
        GameEntity gameEntity = collision.gameObject.GetComponent<GameEntity>();

        if (collision.gameObject.layer == 11 && !gameEntity.IsDead())
        {
            ISlowable slowable = collision.gameObject.GetComponent<ISlowable>();
            slowable?.Slow();
        }

        base.OnTriggerEnter2D(collision);
    }
}
