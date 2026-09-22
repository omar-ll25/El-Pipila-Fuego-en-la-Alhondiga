using UnityEngine.SceneManagement;

public class MainMenuState : IGameState
{
    public void Enter() => SceneManager.LoadScene("MainMenu");
    public void Exit() { }
}

public class LevelMapState : IGameState
{
    public void Enter() => SceneManager.LoadScene("LevelMap");
    public void Exit() { }
}

public class LevelState : IGameState
{
    private int levelIndex;

    public LevelState(int levelIndex)
    {
        this.levelIndex = levelIndex;
    }

    public void Enter()
    {
        GameStateManager.Instance.currentLevelIndex = levelIndex;
        LevelLogger.Instance.LevelStarted(levelIndex);
        SceneManager.LoadScene("Level_0" + levelIndex);
    }

    public void Exit() { }
}

public class VictoryState : IGameState
{
    public void Enter() => SceneManager.LoadScene("VictoryScreen");
    public void Exit() { }
}