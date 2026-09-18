using UnityEngine;
using UnityEngine.UI;

public class LevelNodeButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image buttonImage;
    [SerializeField] Sprite availableSprite;   
    [SerializeField] Sprite completedSprite;   

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
        buttonImage.sprite = completed ? completedSprite : availableSprite;
    }

    public void OnClick()
    {
        mapController.OnLevelSelected(levelNumber);
    }
}