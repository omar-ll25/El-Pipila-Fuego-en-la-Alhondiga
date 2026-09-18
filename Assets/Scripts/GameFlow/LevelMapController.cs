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
            levelNodes[i].Setup(levelNumber, isUnlocked, isCompleted);
        }
    }

    public void OnLevelSelected(int levelNumber)
    {
        GameStateManager.Instance.ChangeState(new LevelState(levelNumber));
    }
}