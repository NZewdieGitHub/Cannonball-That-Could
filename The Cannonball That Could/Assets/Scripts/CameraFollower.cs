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
    }
}
