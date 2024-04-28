using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// A Game Manager
/// </summary>
public class GameManager : MonoBehaviour
{
    // Player fields
    [SerializeField]
    Player player;

    // Camera field
    [SerializeField]
    GameObject vc;

    // Cannon Fields
    [SerializeField]
    GameObject cannon;


    //Pause Panel Fields 
    [SerializeField]
    public GameObject PausePanel;

    // Dead Zone Field
    [SerializeField]
    public DeadZone deadZone;

    // Hold every enemy cannon gameobject in the scene
    [SerializeField]
    List<GameObject> enemyCannons;
    // player ship death field
    public bool finalHit = false;
    // Hud field
    HUD hudScript;

    // Game Over timer 
    public float gameOverTime = 2.5f;
    public bool gameOverTimeRunning = false;
    // Start is called before the first frame update
    void Start()
    { 
        // add self as a respawn event listener
        hudScript = GameObject.FindWithTag("HUD").GetComponent<HUD>();
        hudScript.AddRespawnEventListener(Respawn);
    }

    // Update is called once per frame
    void Update()
    {
        // check if player shot their cannon
        if (player.cannonShot == true)
        {
            //// Fire all enemy cannons
            //foreach(EnemyCannon enemyCannon in enemyCannons) 
            //{
            //    enemyCannon.ShootEnemy();
            //}
            
        }
        // Check if time is running
        if (gameOverTimeRunning == true)
        {
            // check if ball is destroyed
            if (player.ballDestroyed == true)
            {

                if (gameOverTime >= 0)
                {
                    // have count down match the frame count
                    gameOverTime -= Time.deltaTime;
                    StartGameOverTimer(gameOverTime);
                }
                else
                {
                    Time.timeScale = 0;

                }

            }

        }
    }

    /// <summary>
    /// Make player respawn after death
    /// </summary>
    public void Respawn()
    {
        // move cannon ball and camera back to starting position
        player.transform.position = cannon.transform.position;
        // make sure the player can't fire if they lost
        if (player.playerLost == false)
        {
            player.canFire = true;
        }
        else if (player.playerLost == true) 
        {
            player.canFire = false;
            player.isFiring = false;
        }
        if (player.playerWon == false)
        {
            player.canFire = true;
        }
        else if (player.playerWon == true)
        {
            player.canFire = false;
            player.isFiring = false;
            hudScript.timeRunning = false;
            hudScript.TextObject.SetActive(false);
            hudScript.TimeHolder.SetActive(false);
        }
        player.hasFired = false;
        vc.transform.position = new Vector3(-45.4f, 0.37f, -21.64309f);
        // respet player speed
        if (player.isBoosted == true)
        {
            player.accelleration = (player.accelleration / 2);
            player.isBoosted = false;
            // give player back the ability to slowdown
            player.canSlowDown = true;
        }
        player.cannonShot = false;
        if (deadZone.enemyCannonDestroyed == false)
        {
            foreach (GameObject eC in enemyCannons)
            {
                eC.GetComponent<EnemyCannon>().enemyCannonShot = false;
            }
        }
        if (hudScript.respawnedAfterLoss == true)
        {
            Time.timeScale = 0;
        }
        if (player.selfDestructed == true)
        {
            hudScript.TimeHolder.SetActive(false);
        }
        player.slowDownPressed = false;
        player.canMoveHoriz = false;
    }
    /// <summary>
    /// Pauses game
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(false);
    }
    /// <summary>
    /// Resumes game
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }
    /// <summary>
    /// Start timer
    /// </summary>
    private void StartGameOverTimer(float currentTime)
    {
        // increment the current time by one second
        currentTime += 1;
        float seconds = Mathf.FloorToInt(currentTime / 60f);
    }
}
