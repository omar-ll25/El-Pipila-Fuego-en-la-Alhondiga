using UnityEngine;

public class BouncingBulletStrategy : IBulletMovementStrategy
{
    private int bouncesLeft;

    public BouncingBulletStrategy(int maxBounces = 2)
    {
        bouncesLeft = maxBounces;
    }

    public void OnObstacleHit(BulletScript bullet, Rigidbody2D rb2d, Collider2D other)
    {
        if (bouncesLeft <= 0)
        {
            Object.Destroy(bullet.gameObject, 0.01f);
            return;
        }

        bouncesLeft--;

        Vector2 hitPosition = bullet.transform.position;
        Vector2 closestPoint = other.ClosestPoint(hitPosition);
        Vector2 approxNormal = (hitPosition - closestPoint).sqrMagnitude > 0.0001f
            ? (hitPosition - closestPoint).normalized
            : (hitPosition - (Vector2)other.bounds.center).normalized;
        Vector2 reflected = Vector2.Reflect(rb2d.linearVelocity, approxNormal);

        rb2d.linearVelocity = reflected;
        bullet.UpdateRotationToVelocity(reflected);
    }
}