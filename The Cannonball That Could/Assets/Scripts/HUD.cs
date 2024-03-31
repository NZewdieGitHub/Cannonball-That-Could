using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class HUD : MonoBehaviour
{
    #region Fields
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
    public int playerScore = 5;

    // Slider Fields
    public HealthBar enemyHealthSlider;
    public HealthBar playerHealthSlider;

    // Panel Field
    [SerializeField]
    GameObject WinPanel;

    // Game Manager Script
    [SerializeField]
    GameManager gameManager;

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
    public GameObject TextObject;
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

    // Event Fields 
    RespawnEvent respawnEvent = new RespawnEvent();
    #endregion
    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        // score setup
        EnemyUI.SetText("Enemy Health: " + enemyScore.ToString() + "/ 10");
        // score setup
        PlayerUI.SetText("Player Health: " + playerScore.ToString() + "/ 5");
        // Setup initial text
        TimeText.SetText("Get back in: " + exitTime.ToString());
        RespawnText.SetText("Respawning in: " + respawnTime.ToString());
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
    /// Move the regular timer to the screen
    /// </summary>
    public void SpawnTimer()
    {
        // make object visible 
        TimeHolder.SetActive(true);
        if (TimeHolder != null)
        {
            Animator animator = TimeHolder.GetComponent<Animator>();
            // make sure componenet is assigned to panel
            if (animator != null)
            {
                bool timerActivated = animator.GetBool("TimerStarted");
                // inverse animation's current state
                animator.SetBool("TimerStarted", true);
            }
        }
    }
    /// <summary>
    /// Move the regular timer to the screen
    /// </summary>
    public void MoveTimer()
    {
        if (TimeHolder != null)
        {
            Animator animator = TimeHolder.GetComponent<Animator>();
            // make sure componenet is assigned to panel
            if (animator != null)
            {
                bool timerActivated = animator.GetBool("TimerStarted");
                // inverse animation's current state
                animator.SetBool("TimerStarted", false);
            }
        }
    }
    /// <summary>
    /// Move the respawn timer to the screen
    /// </summary>
    public void SpawnRespawnTimer()
    {
        // make object visible 
        RespawnTimeHolder.SetActive(true);
        if (TimeHolder != null)
        {
            Animator animator = RespawnTimeHolder.GetComponent<Animator>();
            // make sure componenet is assigned to panel
            if (animator != null)
            {
                bool timerActivated = animator.GetBool("RespawnTriggered");
                // inverse animation's current state
                animator.SetBool("RespawnTriggered", true);
            }
        }
    }
    /// <summary>
    /// Move the respawn timer to the screen
    /// </summary>
    public void MoveRespawnTimer()
    {
        // make object visible 
        RespawnTimeHolder.SetActive(true);
        if (TimeHolder != null)
        {
            Animator animator = RespawnTimeHolder.GetComponent<Animator>();
            // make sure componenet is assigned to panel
            if (animator != null)
            {
                bool timerActivated = animator.GetBool("RespawnTriggered");
                // inverse animation's current state
                animator.SetBool("RespawnTriggered", false);
            }
        }
    }
    /// <summary>
    /// Start timer
    /// </summary>
    private void StartTimer(float currentTime)
    {
        // increment the current time by one second
        currentTime += 1;
        float seconds = Mathf.FloorToInt(currentTime / 60f);
        // update timer
        TimeText.SetText("Get back in: " + exitTime.ToString("0"));
    }
    /// <summary>
    /// RespawnTimer
    /// </summary>
    private void StartRespawnTimer(float currentTime)
    {
        // increment the current time by one second
        currentTime += 1;
        float seconds = Mathf.FloorToInt(currentTime / 60f);
        // update timer
        RespawnText.SetText("Respawning in: " + respawnTime.ToString("0"));
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
        PlayerUI.SetText("Player Health: " + playerScore.ToString() + "/ 5");
        playerHealthSlider.SetBar(playerScore);

        // check if player score is empty
        if (playerScore <= 0)
        {
            playerScore = 0;
            PlayerUI.SetText("Player Health: " + playerScore.ToString() + "/ 5");
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
        // Reset player's position
        // gameManager.Respawn();
    }
    /// <summary>
    /// Instantiate Win Menu
    /// </summary>
    public void SpawnWinMenu()
    {
        // Make panel visible
        WinPanel.SetActive(true);
        respawnTimeRunning = false;
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
        // Reset player's position
        // gameManager.Respawn();
    }
    #endregion
    #region Eventlisteners
    /// <summary>
    /// Adds listener to the respawn event 
    /// </summary>
    public void AddRespawnEventListener(UnityAction listener)
    {
        respawnEvent.AddListener(listener);
    }
    #endregion
}
