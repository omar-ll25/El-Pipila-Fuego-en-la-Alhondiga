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

        Vector2 normal = GetSurfaceNormal(bullet.transform.position, rb2d.linearVelocity, other);
        Vector2 reflected = Vector2.Reflect(rb2d.linearVelocity, normal);

        rb2d.linearVelocity = reflected;
        bullet.UpdateRotationToVelocity(reflected);
    }

    Vector2 GetSurfaceNormal(Vector2 hitPosition, Vector2 velocity, Collider2D other)
    {
        Vector2 direction = velocity.normalized;
        Vector2 rayOrigin = hitPosition - direction * 1f;
        int layerMask = 1 << other.gameObject.layer;

        RaycastHit2D[] hits = Physics2D.RaycastAll(rayOrigin, direction, 2f, layerMask);
        foreach (RaycastHit2D candidate in hits)
        {
            if (candidate.collider == other)
            {
                return candidate.normal;
            }
        }

        // Alternativa si el rayo no pega en el collider (caso raro)
        Vector2 closestPoint = other.ClosestPoint(hitPosition);
        return (hitPosition - closestPoint).sqrMagnitude > 0.0001f
            ? (hitPosition - closestPoint).normalized
            : (hitPosition - (Vector2)other.bounds.center).normalized;
    }
}