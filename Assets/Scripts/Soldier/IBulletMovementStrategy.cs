using UnityEngine;

public interface IBulletMovementStrategy
{
    void OnObstacleHit(BulletScript bullet, Rigidbody2D rb2d, Collider2D other);
}