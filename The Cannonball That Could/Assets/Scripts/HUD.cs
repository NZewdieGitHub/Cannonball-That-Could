using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    // Player field
    [SerializeField]
    Player player;

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

    // Text Field
    [SerializeField]
    TextMeshProUGUI TimeText;
    [SerializeField]
    GameObject TextObject;
    public float exitTime = 3f;
    public bool timeRunning = false;
    public bool frozenScreen = false;

    // Timer parent object holder
    [SerializeField]
    GameObject TimeHolder;
    [SerializeField]
    GameObject RespawnTimeHolder;
    // Respawn timer fields
    [SerializeField]
    TextMeshProUGUI RespawnText;
    [SerializeField]
    GameObject RespawnTextObject;
    public float respawnTime = 3f;
    public bool respawnTimeRunning = false;

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
        // Check if time is running
        if (timeRunning == true)
        {
            // check if balll isn't destroyed
            if (player.ballDestroyed == false)
            {
                if (exitTime >= 0)
                {
                    // have count down match the frame count
                    exitTime -= Time.deltaTime;
                    StartTimer(exitTime);
                }
                else
                {
                    // check if time reaches 0
                    timeRunning = false;
                    SubtractPlayerPoints(1);
                    // move cannon ball and camera back to starting position
                    respawnEvent.Invoke();
                }

                // Spawn lose menu
                if (playerScore <= 0)
                {
                    SpawnLoseMenu();
                }
            }

        }
        // check if ball is destroyed
        if (player.ballDestroyed == true)
        {
            respawnTimeRunning = true;
            // set exit timer to false 
            if (TextObject.activeInHierarchy)
            {
                TextObject.SetActive(false);
            }
        }
        // Check if respawn time is running
        if (respawnTimeRunning == true)
        {
            if (respawnTime >= 0)
            {
                RespawnTextObject.SetActive(true);
                SpawnRespawnTimer();
                // have count down match the frame count
                respawnTime -= Time.deltaTime;
                StartRespawnTimer(respawnTime);
            }
            else
            {
                // check if time reaches 0
                respawnTimeRunning = false;
                // move cannon ball and camera back to starting position
                respawnEvent.Invoke();
                // reset timer
                RespawnTextObject.SetActive(false);
                MoveRespawnTimer();
                respawnTime = 3f;
                // Make player visible again
                player.gameObject.SetActive(true);
                player.ballDestroyed = false;
            }
        }
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
