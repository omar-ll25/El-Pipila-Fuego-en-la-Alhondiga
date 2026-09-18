using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryController : MonoBehaviour
{
    public void OnExitPressed()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.ChangeState(new MainMenuState());
        }
        else
        {
            // Escena abierta directo en el editor: Start crea el GameStateManager y lleva al menú
            SceneManager.LoadScene("Start");
        }
    }
}
