using UnityEngine;

class SpicyBullet : Bullet
{
    public new void OnTriggerEnter2D(Collider2D collision)
    {
        GameEntity gameEntity = collision.gameObject.GetComponent<GameEntity>();

        if (collision.gameObject.layer == 11 && !gameEntity.IsDead())
        {
            IBurnable burnable = collision.gameObject.GetComponent<IBurnable>();
            burnable?.Burn();
        }

        base.OnTriggerEnter2D(collision);
    }
}
