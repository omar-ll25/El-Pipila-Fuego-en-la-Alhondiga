using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryController : MonoBehaviour
{
    [SerializeField] TMP_Text collectedText;

    void Start()
    {
        if (collectedText == null || GameStateManager.Instance == null) return;

        GameStateManager gsm = GameStateManager.Instance;
        collectedText.text = $"{gsm.TotalCollected} / {gsm.TotalCollectibles} banderas recolectadas";
    }

    public void OnExitPressed()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.ChangeState(new MainMenuState());
        }
        else
        {
            SceneManager.LoadScene("Start");
        }
    }
}
