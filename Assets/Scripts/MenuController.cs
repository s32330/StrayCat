using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        if (GameData.Instance != null)
            GameData.Instance.Clear();
        AudioManager.Instance.PlayMusic("GameplayTheme");
        SceneManager.LoadScene("8");
    }

    public void BackToMenu()
    {
        AudioManager.Instance.PlayMusic("Menu");
        SceneManager.LoadScene("Menu");
    }

    public void HowToPlayToMenu()
    {
       
        SceneManager.LoadScene("Menu");
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
