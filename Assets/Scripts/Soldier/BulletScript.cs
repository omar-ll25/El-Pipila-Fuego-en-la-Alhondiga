using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody2D rb2d;

    float destroyTime;

    int damage = 1;
    public int Damage => damage; 

    [SerializeField] float bulletSpeed;
    [SerializeField] Vector2 bulletDirection;
    [SerializeField] float destroyDelay;

    IBulletMovementStrategy movementStrategy = new StraightBulletStrategy(); 

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime < 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetBulletSpeed(float speed) => this.bulletSpeed = speed;
    public void SetBulletDirection(Vector2 direction) => this.bulletDirection = direction;
    public void SetDamageValue(int damage) => this.damage = damage;
    public void SetDestroyDelay(float delay) => this.destroyDelay = delay;

    public void SetMovementStrategy(IBulletMovementStrategy strategy)
    {
        this.movementStrategy = strategy;
    }

    public void Shoot()
    {
        float angle = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        rb2d.linearVelocity = bulletDirection * bulletSpeed;
        destroyTime = destroyDelay;
    }

    public void UpdateRotationToVelocity(Vector2 velocity) 
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject, 0.01f);
            return;
        }

        movementStrategy.OnObstacleHit(this, rb2d, other); 
    }
}