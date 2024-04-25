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
    public int enemyScore = 20;

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
    public GameObject TimeHolder;
    [SerializeField]
    public GameObject RespawnTimeHolder;
    // Respawn timer fields
    [SerializeField]
    TextMeshProUGUI RespawnText;
    [SerializeField]
    GameObject RespawnTextObject;
    public float respawnTime = 3f;
    public bool respawnTimeRunning = false;

    // respawn after losing fields
    public bool respawnedAfterLoss = false;

    // Event Fields 
    RespawnEvent respawnEvent = new RespawnEvent();

    // Cannon fire count
    [SerializeField]
    public TextMeshProUGUI ShotCount;
    [SerializeField]
    public Image CountHolder;
    [SerializeField]
    public TextMeshProUGUI shotResults;
    public string shotText = "Shot Count: ";
    public int numShots = 0;

    [SerializeField]
    GameObject GoldTrophy;
    [SerializeField]
    GameObject SilverTrophy;
    [SerializeField]
    GameObject BronzeTrophy;

    public bool goldAchieved = false;
    public bool silverAchieved = false;
    public bool bronzeAchieved = false;
    #endregion
    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        // score setup
        EnemyUI.SetText("Enemy Health: " + enemyScore.ToString() + "/ 20");
        // score setup
        PlayerUI.SetText("Player Health: " + playerScore.ToString() + "/ 5");
        // Setup initial text
        TimeText.SetText("Get back in: " + exitTime.ToString());
        RespawnText.SetText("Respawning in: " + respawnTime.ToString());
        // Shot Count Setup
        ShotCount.SetText("Shot Count: " + numShots.ToString());
        // Add self as health reduced event listener
        EventManager.AddHealthReducedEventListener(SubtractEnemyPoints);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if time is running
        if (timeRunning == true)
        {
            // check if ball isn't destroyed
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
                    if (playerScore > 1) 
                    {
                        // check if time reaches 0
                        timeRunning = false;
                        SubtractPlayerPoints(1);
                        // move cannon ball and camera back to starting position
                        respawnEvent.Invoke();
                    }
                    else if (playerScore <= 1) 
                    {
                        player.playerLost = true;
                        SubtractPlayerPoints(1);
                        respawnEvent.Invoke();
                        respawnTimeRunning = false;
                        RespawnTextObject.SetActive(false);
                        RespawnTimeHolder.SetActive(false);
                        if (player.playerWon == false)
                        {
                            SpawnLoseMenu();
                            player.canFire = false;
                            respawnedAfterLoss = true;
                        }
                    }
                    
                }

            }

        }
        // check if ball is destroyed
        if (player.ballDestroyed == true)
        {
            if (player.playerWon == false || player.playerLost == false)
            {
                respawnTimeRunning = true;
                // set exit timer to false 
                if (TextObject.activeInHierarchy)
                {
                    TextObject.SetActive(false);
                }
            }
            else if (player.playerWon == true || player.playerLost == true) 
            {
                respawnTimeRunning = false;
                RespawnTextObject.SetActive(false);
                RespawnTimeHolder.SetActive(false);
            }
        }
        // Check if respawn time is running
        if (respawnTimeRunning == true)
        {
            if (player.lastPlayerHit == false)
            {
                if (respawnTime >= 0)
                {
                    // check enemy health
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
        // Spawn lose menu if player score is below 0
        if (playerScore <= 0)
        {
            // make sure the health stays 0
            playerScore = 0;
            // score setup
            PlayerUI.SetText("Player Health: " + playerScore.ToString() + "/ 5");
            if (player.playerWon == false)
            {
                SpawnLoseMenu();
            }

        }
        // If enemy's defeated
        if (enemyScore <= 0)
        {
            // make sure the health stays 0
            enemyScore = 0;
            // score setup
            EnemyUI.SetText("Enemy Health: " + enemyScore.ToString() + "/ 10");
            // Display the win screen
            SpawnWinMenu();
            // deactivate timer
            player.playerWon = true;
        }

        // check if the final hit was landed
        if (player.lastPlayerHit == true)
        {
            // don't start the timer
            timeRunning = false;
            respawnTimeRunning = false;
        }

        // Check if gold was achieved
        if (goldAchieved == true)
        {
            GoldTrophy.SetActive(true);
        }
        // Check if silver was achieved
        if (silverAchieved == true)
        {
            SilverTrophy.SetActive(true);
        }
        // Check if silver was achieved
        if (bronzeAchieved == true)
        {
            BronzeTrophy.SetActive(true);
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
    /// Move the respawn timer away from the screen
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
        // Deactivate all timers
        respawnTimeRunning = false;
        RespawnTextObject.SetActive(false);
        RespawnTimeHolder.SetActive(false);
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
        // Deactivate all timers
        respawnTimeRunning = false;
        RespawnTextObject.SetActive(false);
        RespawnTimeHolder.SetActive(false);
        // Make panel visible
        WinPanel.SetActive(true);
        respawnTimeRunning = false;
        // Make Pause Button Invisible
        PauseButton.SetActive(false);
        // determine player score
        if (numShots <= 5)
        {
            goldAchieved = true;
            shotResults.SetText("You beat the enemy with: " + numShots.ToString() + " shots!");
        }
        else if (numShots >= 5 && numShots <= 10) 
        {
            silverAchieved = true;
            shotResults.SetText("You beat the enemy with: " + numShots.ToString() + " shots!");
        }
        else
        {
            bronzeAchieved = true;
            shotResults.SetText("You beat the enemy with: " + numShots.ToString() + " shots!");
        }
        //else if ( numShots < 10)
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
    public void AddShotCount(int count)
    {
        numShots += count;
        // Shot Count Setup
        ShotCount.SetText("Shot Count: " + numShots.ToString());
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
