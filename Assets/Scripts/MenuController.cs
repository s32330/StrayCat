using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        if (GameData.Instance != null)
            GameData.Instance.Clear();

        SceneManager.LoadScene("8");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
