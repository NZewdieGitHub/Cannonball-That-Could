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
    public bool isFiring = false;
    private float firingPower = 8f;
    private float fireTime = 2f;

    // collider fields
    public CircleCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        // get the rigidbody2D component
        rb2d = GetComponent<Rigidbody2D>();
        // get camera component
        cf = GetComponent<CameraFollower>();
        // get collider component
        collider = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if cannon has fired
        if (isFiring) 
        {
            return;
        }

        // movement input setup
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // restart functionality (for testing purposes)
        if (Input.GetKey(KeyCode.Escape))
        {
            RestartLevel();
        }



        // check if the cannon has fired or not
        // Slowdown funtion
        if (Input.GetKeyDown(KeyCode.Space))
        {
            slowDownPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            slowDownPressed = false;
        }
        
        
        if (isFiring == false && canFire == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Fire());
            }
        }
    }
    /// <summary>
    /// Make player move automatically
    /// </summary>
    private void FixedUpdate()
    {
        // check if cannon has fired
        if (isFiring)
        {
            return;
        }
        if (canFire == false)
        {
            // move cannon ball freely
            rb2d.MovePosition(rb2d.position + movement * accelleration * Time.deltaTime);
        }
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
    private IEnumerator Fire()
    {
        canFire = false;
        isFiring = true;
        float orginalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(transform.localScale.x * firingPower, 0f);

        // stop dashing for a short amount of time
        yield return new WaitForSeconds(fireTime);
        rb2d.gravityScale = orginalGravity;
        isFiring = false;
        
    }

}
