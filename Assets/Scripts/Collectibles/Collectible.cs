using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] int index; 

    bool collected;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        collected = true;
        LevelCollectibles.Instance.Collect(index);
        gameObject.SetActive(false);
    }
}
