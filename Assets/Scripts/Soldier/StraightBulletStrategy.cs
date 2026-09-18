using UnityEngine;

public class StraightBulletStrategy : IBulletMovementStrategy
{
    public void OnObstacleHit(BulletScript bullet, Rigidbody2D rb2d, Collider2D other)
    {
        Object.Destroy(bullet.gameObject, 0.01f);
    }
}