using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("8"); // nazwa sceny z gr¹
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu"); 
    }

    public void HowToPlay()
    {
        // Mo¿esz wyœwietliæ panel z instrukcj¹
        Debug.Log("Pokaz instrukcje gry");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Wyjœcie z gry");
    }
}
