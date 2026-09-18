using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject creditsPanel;

    [Header("Audio")]
    [SerializeField] float currentVolume = 1f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMainMenu()
    {
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        Time.timeScale = 1f;
        GameStateManager.Instance.ChangeState(new MainMenuState());
    }

    public void OpenCredits()
    {
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackToSettingsFromCredits()
    {
        creditsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OnAudioVolumeChanged(float value)
    {
        currentVolume = value;
        AudioListener.volume = value;
    }

    public float GetCurrentVolume() => currentVolume;
}