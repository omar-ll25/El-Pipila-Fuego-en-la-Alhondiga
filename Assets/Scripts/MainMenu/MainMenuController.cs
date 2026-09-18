using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayPressed()
    {
        GameStateManager.Instance.ChangeState(new LevelMapState());
    }

    public void OnSettingsPressed()
    {
        SettingsManager.Instance.OpenSettings();
    }

    public void OnQuitPressed()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}