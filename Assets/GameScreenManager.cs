using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScreenManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject gameOverScreen;
    public GameObject gameWinScreen;
    
    void OnGameOver()
    {
        Time.timeScale = 0;
        
        gameOverScreen.SetActive(true);
    }

    void OnGameWin()
    {
        Time.timeScale = 0;
        gameWinScreen.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager.GameWin += OnGameWin;
        gameManager.GameLost += OnGameOver;
    }

    public void RestartDay()
    {
        Time.timeScale = 1;
        
        // Restart this scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void NextDay()
    {
        
    }

    public void MainMenu()
    {
        // TODO: Go to Main Menu.
    }
}
