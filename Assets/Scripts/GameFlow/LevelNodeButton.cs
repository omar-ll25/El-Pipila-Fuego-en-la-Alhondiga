using UnityEngine;
using UnityEngine.UI;

public class LevelNodeButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image buttonImage;
    [SerializeField] Sprite availableSprite;   
    [SerializeField] Sprite completedSprite;
    [SerializeField] GameObject[] flagIcons;   

    LevelMapController mapController;
    int levelNumber;

    void Awake()
    {
        mapController = FindFirstObjectByType<LevelMapController>();
    }

    public void Setup(int level, bool unlocked, bool completed, bool collectible1, bool collectible2)
    {
        levelNumber = level;
        button.interactable = unlocked;
        buttonImage.sprite = completed ? completedSprite : availableSprite;

        if (flagIcons.Length > 0) flagIcons[0].SetActive(collectible1);
        if (flagIcons.Length > 1) flagIcons[1].SetActive(collectible2);
    }

    public void OnClick()
    {
        mapController.OnLevelSelected(levelNumber);
    }
}