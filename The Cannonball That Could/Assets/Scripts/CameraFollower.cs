using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;

public class CameraFollower : MonoBehaviour
{
    public float cameraAccelleration = 100f;

    // Player field
    [SerializeField]
    Player player;

    // Cannon Field
    [SerializeField]
    GameObject cannon;

    // Game Manager Script
    [SerializeField]
    GameManager gameManager;

    // Text Field
    [SerializeField]
    TextMeshProUGUI TimeText;
    [SerializeField]
    GameObject TextObject;
    public float exitTime = 3f;
    public bool timeRunning = false;
    public bool frozenScreen = false;

    // Respawn timer fields
    [SerializeField]
    TextMeshProUGUI RespawnText;
    [SerializeField]
    GameObject RespawnTextObject;
    public float respawnTime = 3f;
    public bool respawnTimeRunning = false;

    // HUD fields
    HUD hud = new HUD();

    // Event Fields 
    RespawnEvent respawnEvent = new RespawnEvent();

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        // Setup initial text
        TimeText.SetText("Get back in: " + exitTime.ToString());
        RespawnText.SetText("Respawning in: " + respawnTime.ToString());
        // Save reference to HUD Script
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // if player presses and holds space
        if (player.slowDownPressed == true && player.hasFired == true)
        {
            // check if the ball hasn't been destroyed
            if (player.ballDestroyed == false) 
            { 
                // move camera along the x-axis at slower speed
                cameraAccelleration = 2f;
                transform.Translate(Vector2.right * cameraAccelleration * Time.deltaTime);
                Debug.Log("Camera's slowed down");
            }

        }
        else if (player.slowDownPressed == false && player.hasFired == true)
        {
            // check if the ball hasn't been destroyed
            if (player.ballDestroyed == false)
            {
                cameraAccelleration = 8f;
                // move camera along the x-axis
                transform.Translate(Vector2.right * cameraAccelleration * Time.deltaTime);
                Debug.Log("Camera's sped up");
            }
        }
        
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
                    hud.SubtractPlayerPoints(1);
                    // move cannon ball and camera back to starting position
                    respawnEvent.Invoke();
                }

                // Spawn lose menu
                if (hud.playerScore <= 0)
                {
                    hud.SpawnLoseMenu();
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
                respawnTime = 3f;
                // Make player visible again
                player.gameObject.SetActive(true);
                player.ballDestroyed = false;
            }
        }
    }
    /// <summary>
    /// Used to determine if player left camera view
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        // make sure only the player exits
        if (collision == player.collider)
        {
            // check if ball hasn't been destroyed or not
            if (player.ballDestroyed == false)
            {
                // Make timer visible 
                TextObject.SetActive(true);

                // count down timer
                timeRunning = true;
            }

        }
    }
    /// <summary>
    /// Used to determine if player left camera view
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        // make sure only the player exits
        if (collision == player.collider)
        {
            // Make Object invisible 
            TextObject.SetActive(false);

            // deactivate timer
            timeRunning = false;

            // reset timer count
            exitTime = 3f;
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
    /// Adds listener to the respawn event 
    /// </summary>
    public void AddRespawnEventListener(UnityAction listener)
    {
        respawnEvent.AddListener(listener);
    }
}
