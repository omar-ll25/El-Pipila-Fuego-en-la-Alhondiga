using UnityEngine;

public class FireHazard : MonoBehaviour
{
    [SerializeField] int damage = 1;

    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
