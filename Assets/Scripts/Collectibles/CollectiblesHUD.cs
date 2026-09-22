using UnityEngine;
using UnityEngine.UI;

public class CollectiblesHUD : MonoBehaviour
{
    public static CollectiblesHUD Instance { get; private set; }

    [SerializeField] Image[] icons;

    [SerializeField] Color collectedNowColor = Color.white;
    [SerializeField] Color gottenBeforeColor = new Color(1f, 1f, 1f, 0.4667f);
    [SerializeField] Color notYetColor = new Color(0f, 0f, 0f, 0.5020f);

    void Awake()
    {
        Instance = this;
    }

    public void Refresh(bool[] collectedThisRun)
    {
        GameStateManager gsm = GameStateManager.Instance;
        int level = gsm != null ? gsm.currentLevelIndex : 0;

        for (int i = 0; i < icons.Length; i++)
        {
            bool collectedNow = i < collectedThisRun.Length && collectedThisRun[i];
            bool gottenBefore = gsm != null && gsm.IsCollectibleSaved(level, i);

            if (collectedNow) icons[i].color = collectedNowColor;
            else if (gottenBefore) icons[i].color = gottenBeforeColor;
            else icons[i].color = notYetColor;
        }
    }
}
