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

    // HUD fields
    HUD hud = new HUD();

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
       
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
            // check if the ball hasn't been destroyed or boosted
            if (player.ballDestroyed == false && player.isBoosted == false)
            {
                cameraAccelleration = 20f;
                // move camera along the x-axis
                transform.Translate(Vector2.right * cameraAccelleration * Time.deltaTime);
                Debug.Log("Camera's sped up");
            }
            //    else if (player.ballDestroyed == false && player.isBoosted == true)
            //    {
            //        cameraAccelleration = 16f;
            //        // move camera along the x-axis
            //        transform.Translate(Vector2.right * cameraAccelleration * Time.deltaTime);
            //        Debug.Log("Camera's bossted");
            //    }
            //}

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
                hud.SpawnTimer();
                hud.TextObject.SetActive(true);
                

                // count down timer
                hud.timeRunning = true;
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
            hud.TextObject.SetActive(false);
            hud.MoveTimer();
            // deactivate timer
            hud.timeRunning = false;

            // reset timer count
            hud.exitTime = 3f;
        }
        
    }
    
}
