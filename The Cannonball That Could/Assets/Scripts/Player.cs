using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{

    // movement fields
    public Rigidbody2D rb2d;
    public float accelleration = 4f;
    Vector2 movement;
    public string levelName;

    // Camera field
    CameraFollower cf;
    public bool slowDownPressed = false;

    // Firing fields
    private bool canFire = true;
    private bool hasFired = false;
    private float firingPower = 6f;

    // Start is called before the first frame update
    void Start()
    {
        // get the rigidbody2D component
        rb2d = GetComponent<Rigidbody2D>();
        cf = GetComponent<CameraFollower>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // movement input setup
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // restart functionality (for testing purposes)
        if (Input.GetKey(KeyCode.Escape))
        {
            RestartLevel();
        }

        // Slowdown funtion
        if (Input.GetKeyDown(KeyCode.Space))
        {
            slowDownPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            slowDownPressed = false;
        }

    }
    /// <summary>
    /// Make player move automatically
    /// </summary>
    private void FixedUpdate()
    {
        // move cannon ball freely
        rb2d.MovePosition(rb2d.position + movement * accelleration * Time.deltaTime);
    }
    /// <summary>
    /// Give player the ability to restart level
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(levelName);
    }

    /// <summary>
    /// Use to shoot cannon ball
    /// </summary>
    private void Fire()
    {
        canFire = false;
    }

}
