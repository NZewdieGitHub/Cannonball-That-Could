using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    public string SceneName;
    [SerializeField]
    public GameObject InstructionsPanel;

    // Animator fields
    Animator animator;
    bool isActivated = false;
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
    /// <summary>
    /// Opens instructions panel
    /// </summary>
    public void OpenInstructions()
    {
        // check if component has animation
        if (InstructionsPanel != null)
        {
            animator = InstructionsPanel.GetComponent<Animator>();
            // make sure componenet is assigned to panel
            if (animator != null)
            {
                isActivated = animator.GetBool("InstructionsOpened");
                // inverse animation's current state
                animator.SetBool("InstructionsOpened", !isActivated);
            }
        }
    }
    /// <summary>
    /// Closes the instructions menu
    /// </summary>
    public void CloseInstructions()
    {
        
        if (InstructionsPanel != null)
        {
            Animator animator = InstructionsPanel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isActivated = animator.GetBool("InstructionsOpened");
                // inverse animation's current state
                animator.SetBool("InstructionsOpened", false);
            }
        }
        
    }
}
