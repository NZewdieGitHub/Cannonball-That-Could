using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    // Text Updating fields (Enemy)
    [SerializeField]
    public TextMeshProUGUI EnemyUI;
    public int enemyScore = 10;

    // Text Updating fields (Player)
    [SerializeField]
    public TextMeshProUGUI PlayerUI;
    public int playerScore = 3;

    // Slider Fields
    public HealthBar enemyHealthSlider;
    public HealthBar playerHealthSlider;

    // Panel Field
    [SerializeField]
    GameObject WinPanel;

    // Panel Setup
    [SerializeField]
    GameObject LosePanel;

    // Pause Button Field
    [SerializeField]
    GameObject PauseButton;


    // Start is called before the first frame update
    void Start()
    {
        // score setup
        EnemyUI.SetText("Enemy Health: " + enemyScore.ToString() + "/ 10");
        // score setup
        PlayerUI.SetText("Player Health: " + playerScore.ToString() + "/ 3");

        // Add self as health reduced event listener
        EventManager.AddHealthReducedEventListener(SubtractEnemyPoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Take away points from Enemy
    /// </summary>
    public void SubtractEnemyPoints(int points)
    {
        // updated score
        enemyScore -= points;
        EnemyUI.SetText("Enemy Health: " + enemyScore.ToString() + "/ 10");
        enemyHealthSlider.SetBar(enemyScore);
    }

    /// <summary>
    /// Take away points from Player
    /// </summary>
    public void SubtractPlayerPoints(int points)
    {
        // updated score
        playerScore -= points;
        PlayerUI.SetText("Player Health: " + playerScore.ToString() + "/ 3");
        playerHealthSlider.SetBar(playerScore);

        // check if player score is empty
        if (playerScore <= 0)
        {
            playerScore = 0;
            PlayerUI.SetText("Player Health: " + playerScore.ToString() + "/ 3");
        }
    }
    /// <summary>
    /// Instantiate Lose Menu
    /// </summary>
    public void SpawnLoseMenu()
    {
        // Make panel visible
        LosePanel.SetActive(true);
        // Make Pause Button Invisible
        PauseButton.SetActive(false);
        // Make Pause Button Invisible
        PauseButton.SetActive(false);
        if (LosePanel != null)
        {
            Animator animator = LosePanel.GetComponent<Animator>();
            // make sure componenet is assigned to panel
            if (animator != null)
            {
                bool isActivated = animator.GetBool("LoseActivated");
                // inverse animation's current state
                animator.SetBool("LoseActivated", !isActivated);
            }
        }
    }
    /// <summary>
    /// Instantiate Win Menu
    /// </summary>
    public void SpawnWinMenu()
    {
        // Make panel visible
        WinPanel.SetActive(true);
        CameraFollower cf = new CameraFollower();
        cf.respawnTimeRunning = false;
        // Make Pause Button Invisible
        PauseButton.SetActive(false);
        if (WinPanel != null) 
        {
            Animator animator = WinPanel.GetComponent<Animator>();
            // make sure componenet is assigned to panel
            if (animator != null) 
            {
                bool isActivated = animator.GetBool("WinActivated");
                // inverse animation's current state
                animator.SetBool("WinActivated", !isActivated);
            }
        }
    }
 
}
