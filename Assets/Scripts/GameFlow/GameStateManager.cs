using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    private IGameState currentState;

    public int currentLevelIndex;
    public int totalLevels = 4;

    public int highestUnlockedLevel = 1; 
    public void UnlockNextLevel(int completedLevel)
    {
        if (completedLevel + 1 > highestUnlockedLevel)
        {
            highestUnlockedLevel = completedLevel + 1;
        }
    }

    public const int CollectiblesPerLevel = 2;

    bool[] savedCollectibles;

    public int TotalCollectibles => totalLevels * CollectiblesPerLevel;

    public int TotalCollected
    {
        get
        {
            int count = 0;
            foreach (bool saved in savedCollectibles)
            {
                if (saved) count++;
            }
            return count;
        }
    }

    int CollectibleSlot(int level, int index)
    {
        if (level < 1 || level > totalLevels) return -1;
        if (index < 0 || index >= CollectiblesPerLevel) return -1;
        return (level - 1) * CollectiblesPerLevel + index;
    }

    public bool IsCollectibleSaved(int level, int index)
    {
        int slot = CollectibleSlot(level, index);
        return slot >= 0 && savedCollectibles[slot];
    }

    public void CommitCollectibles(int level, bool[] collectedThisRun)
    {
        for (int i = 0; i < collectedThisRun.Length; i++)
        {
            int slot = CollectibleSlot(level, i);
            if (slot >= 0 && collectedThisRun[i])
            {
                savedCollectibles[slot] = true;
            }
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        savedCollectibles = new bool[totalLevels * CollectiblesPerLevel];
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        ChangeState(new MainMenuState());
    }

    public void ChangeState(IGameState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}