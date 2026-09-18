using System.Collections;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] float fireInterval = 2f;
    [SerializeField] float shootAnimDuration = 0.2f;
    [SerializeField] Vector2 shootDirection = new Vector2(1f, -1f).normalized;

    /// <summary>
    /// BulletHandling
    /// </summary>
    [SerializeField] int bulletDamage = 1;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float bulletDestroyDelay = 3f;
    [SerializeField] Transform bulletShootPos;
    [SerializeField] GameObject bulletPrefab;

    float fireTimer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        fireTimer = fireInterval;
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            fireTimer = fireInterval;
            StartCoroutine(ShootRoutine());
        }
    }

    IEnumerator ShootRoutine()
    {
        animator.Play("Shoot");
        ShootBullet();
        yield return new WaitForSeconds(shootAnimDuration);
        animator.Play("Idle");
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletShootPos.position, Quaternion.identity);
        bullet.name = bulletPrefab.name;

        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        bulletScript.SetDamageValue(bulletDamage);
        bulletScript.SetBulletSpeed(bulletSpeed);
        bulletScript.SetBulletDirection(shootDirection);
        bulletScript.SetDestroyDelay(bulletDestroyDelay);
        bulletScript.Shoot();
    }
}
