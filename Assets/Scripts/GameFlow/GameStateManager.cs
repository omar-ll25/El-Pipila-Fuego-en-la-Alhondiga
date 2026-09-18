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

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
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