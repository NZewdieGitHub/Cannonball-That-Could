using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    public string SceneName;

    /// <summary>
    /// Restart Game
    /// </summary>
    public void RestartGame()
    {
        // Confirm the scene has been loaded
        Debug.Log("Game has restarted. Have Fun.");

        // if game is paused 
        if (Time.timeScale == 0f)
        {
            // resume game's runtime
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(SceneName);
    }
    /// <summary>
    /// Starts game through scene 0
    /// </summary>
    public void StartGame()
    {
        
        SceneManager.LoadScene(SceneName);
    }
    /// <summary>
    /// Quits application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// Go to title screen
    /// </summary>
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
