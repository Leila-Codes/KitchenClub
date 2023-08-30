using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Day01");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}