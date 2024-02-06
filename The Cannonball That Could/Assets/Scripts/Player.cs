using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    // movement fields
    public Rigidbody2D rb2d;
    public float accelleration = 4f;
    Vector2 movement;
    public string levelName;

    // Camera field
    [SerializeField]
    GameObject vc;
    CameraFollower cf;
    public bool slowDownPressed = false;

    // Firing fields
    private bool canFire = true;
    public bool isFiring = false;
    private float firingPower = 8f;
    private float fireTime = 2f;

    // collider fields
    public CircleCollider2D collider;

    // Text Updating fields
    [SerializeField]
    public TextMeshProUGUI EnemyUI;
    public int enemyScore = 3;

    // Enemy Field
    [SerializeField]
    Enemy enemy;

    // Cannon Fields
    [SerializeField]
    GameObject cannon;

    // Panel Field
    [SerializeField]
    GameObject WinPanel;

    // Start is called before the first frame update
    void Start()
    {
        // get the rigidbody2D component
        rb2d = GetComponent<Rigidbody2D>();
        // get camera component
        cf = GetComponent<CameraFollower>();
        // get collider component
        collider = gameObject.GetComponent<CircleCollider2D>();
        // score setup
        EnemyUI.SetText("Enemy Health: " + enemyScore.ToString());
        Time.timeScale = 1;
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
        
        // Exit Functionality
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Application.Quit();
        }


        if (isFiring == false && canFire == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Fire());
            }
        }

        // If player can fire itself
        if (canFire == true) 
        {
            transform.position = Vector3.zero;
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
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            
        }
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
    /// <summary>
    /// Used for colliding with the enemy ship
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
     
        // if player collides with enemy
       if (collision.gameObject.CompareTag("Enemy"))
       {

                // updated score
                enemyScore -= 1;
                EnemyUI.SetText("Enemy Health: " + enemyScore.ToString());

                // reposition player
                // transform.position = cannon.transform.position;
                Destroy(gameObject);
                Destroy(collision.gameObject);

                canFire = true;
                vc.transform.position = new Vector3(-8.4989f, 0.37f, -21.64309f);
                // If enemy's defeated
                if (enemyScore <= 0)
                {
                    // Display the win screen
                    SpawnWinMenu();
                }

        }
        // if player collides with TNT
        if (collision.gameObject.CompareTag("TNT"))
        {
            // updated score
            enemyScore -= 2;
            EnemyUI.SetText("Enemy Health: " + enemyScore.ToString());

            // reposition player
            transform.position = cannon.transform.position;
            canFire = true;
            vc.transform.position = new Vector3(-8.4989f, 0.37f, -21.64309f);

            Destroy(collision.gameObject);
            // If enemy's defeated
            if (enemyScore <= 0)
            {
                // Display the win screen
                SpawnWinMenu();
            }
        }

    }
    /// <summary>
    /// Instantiate Win Menu
    /// </summary>
    public void SpawnWinMenu()
    {
        WinPanel.SetActive(true);
        Time.timeScale = 0;
    }
    
}
