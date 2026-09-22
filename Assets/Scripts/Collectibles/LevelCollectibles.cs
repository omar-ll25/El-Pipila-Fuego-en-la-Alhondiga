using UnityEngine;

public class LevelCollectibles : MonoBehaviour
{
    public static LevelCollectibles Instance { get; private set; }

    bool[] collectedThisRun = new bool[GameStateManager.CollectiblesPerLevel];

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        RefreshHUD();
    }

    public void Collect(int index)
    {
        if (index < 0 || index >= collectedThisRun.Length) return;

        collectedThisRun[index] = true;
        RefreshHUD();
    }

    public void Commit(int levelNumber)
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.CommitCollectibles(levelNumber, collectedThisRun);
    }

    void RefreshHUD()
    {
        if (CollectiblesHUD.Instance != null)
            CollectiblesHUD.Instance.Refresh(collectedThisRun);
    }
}
