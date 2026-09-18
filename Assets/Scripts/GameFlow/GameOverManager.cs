using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    [SerializeField] GameObject gameOverPanel;

    void Awake()
    {
        Instance = this;
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnRetryPressed()
    {
        Time.timeScale = 1f;
        GameStateManager.Instance.ChangeState(new LevelState(GameStateManager.Instance.currentLevelIndex));
    }

    public void OnSettingsPressed()
    {
        if (SettingsManager.Instance != null)
            SettingsManager.Instance.OpenSettings();
    }

    public void OnMainMenuPressed()
    {
        Time.timeScale = 1f;
        GameStateManager.Instance.ChangeState(new MainMenuState());
    }
}