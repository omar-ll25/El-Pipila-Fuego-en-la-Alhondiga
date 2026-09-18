using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    [SerializeField] int levelNumber;
    [SerializeField] Animator fireAnimator; 

    bool triggered;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        triggered = true;

        if (fireAnimator != null)
            fireAnimator.Play("Burning");

        Invoke(nameof(CompleteLevel), 1.0f);
    }

    void CompleteLevel()
    {
        GameStateManager.Instance.UnlockNextLevel(levelNumber);

        if (levelNumber >= GameStateManager.Instance.totalLevels)
        {
            GameStateManager.Instance.ChangeState(new VictoryState());
        }
        else
        {
            GameStateManager.Instance.ChangeState(new LevelMapState());
        }
    }
}