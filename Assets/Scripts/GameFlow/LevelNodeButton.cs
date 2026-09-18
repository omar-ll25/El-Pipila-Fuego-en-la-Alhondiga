using UnityEngine;
using UnityEngine.UI;

public class LevelNodeButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] GameObject lockedIcon;
    [SerializeField] GameObject completedIcon;

    LevelMapController mapController;
    int levelNumber;

    void Awake()
    {
        mapController = FindFirstObjectByType<LevelMapController>();
    }

    public void Setup(int level, bool unlocked, bool completed)
    {
        levelNumber = level;
        button.interactable = unlocked;
        lockedIcon.SetActive(!unlocked);
        completedIcon.SetActive(completed);
    }

    public void OnClick()
    {
        mapController.OnLevelSelected(levelNumber);
    }
}