using UnityEngine;

public class LevelMapController : MonoBehaviour
{
    [SerializeField] LevelNodeButton[] levelNodes; 

    void Start()
    {
        int unlocked = GameStateManager.Instance.highestUnlockedLevel;

        for (int i = 0; i < levelNodes.Length; i++)
        {
            int levelNumber = i + 1;
            bool isUnlocked = levelNumber <= unlocked;
            bool isCompleted = levelNumber < unlocked;
            bool collectible1 = GameStateManager.Instance.IsCollectibleSaved(levelNumber, 0);
            bool collectible2 = GameStateManager.Instance.IsCollectibleSaved(levelNumber, 1);
            levelNodes[i].Setup(levelNumber, isUnlocked, isCompleted, collectible1, collectible2);
        }
    }

    public void OnLevelSelected(int levelNumber)
    {
        GameStateManager.Instance.ChangeState(new LevelState(levelNumber));
    }

    public void OnSettingsPressed()
    {
        if (SettingsManager.Instance != null)
            SettingsManager.Instance.OpenSettings();
    }
}