using UnityEngine;
using UnityEngine.SceneManagement;
public class MainUIController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    // Update is called once per frame
    public void OnExit()
    {
        print("Fechando o jogo...");
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
