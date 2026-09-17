using UnityEngine;

public class SoldierController : MonoBehaviour
{
    /// <summary>
    /// BulletHandling
    /// </summary>
    [SerializeField] int bulletDamage = 1;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] Transform bulletShootPos;
    [SerializeField] GameObject bulletPrefab;
}
