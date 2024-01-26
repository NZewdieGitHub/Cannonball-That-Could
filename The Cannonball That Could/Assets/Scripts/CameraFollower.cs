using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollower : MonoBehaviour
{
    public float cameraAccelleration = 100f;

    // Player field
    [SerializeField]
    Player player;

    // Cannon Field
    [SerializeField]
    GameObject cannon;

    // Text Field
    [SerializeField]
    TextMeshProUGUI TimeText;
    [SerializeField]
    GameObject TextObject;
    public float exitTime = 3f;
    public bool timeRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        // Setup initial text
        TimeText.SetText("Get back in: " + exitTime.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        // if player presses and holds space
        if (player.slowDownPressed == true)
        {
            // move camera along the x-axis at slower speed
            cameraAccelleration = 2f;
            transform.Translate(Vector2.right * cameraAccelleration * Time.deltaTime);
            Debug.Log("Camera's slowed down");
        }
        else if (player.slowDownPressed == false) 
        {
            cameraAccelleration = 4f;
            // move camera along the x-axis
            transform.Translate(Vector2.right * cameraAccelleration * Time.deltaTime);
            Debug.Log("Camera's sped up");
        }
        
        // Check if time is running
        if (timeRunning == true) 
        {
            // have count down match the frame count
            exitTime -= Time.deltaTime;
            StartTimer(exitTime);

            // check if time reaches 0
            if (exitTime <= 0)
            {
                // move cannon ball and camera back to starting position
                player.transform.position = new Vector3(-8.4989f, 0.37f, 0);
                transform.position = new Vector3(-8.4989f, 0.37f, -21.64309f); 
            }
        }
    }
    /// <summary>
    /// Used to determine if player left camera view
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Make timer visible 
        TextObject.SetActive(true);

        // count down timer
        timeRunning = true;
    }
    /// <summary>
    /// Used to determine if player left camera view
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Make Object invisible 
        TextObject.SetActive(false);

        // deactivate timer
        timeRunning = false;

        // reset timer count
        exitTime = 3f;
    }
    /// <summary>
    /// Start timer
    /// </summary>
    private void StartTimer(float currentTime)
    {
        // increment the current time by one second
        currentTime += 1;
        // update timer
        TimeText.SetText("Get back in: " + exitTime.ToString("0"));
    }
}
