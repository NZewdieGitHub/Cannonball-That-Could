using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // movement fields
    public Rigidbody2D rb2d;
    public float accelleration = 12f;
    Vector2 movement;
    public string levelName;

    // Camera field
    [SerializeField]
    GameObject vc;
    [SerializeField]
    CameraFollower cf;
    public bool slowDownPressed = false;

    // Firing fields
    public bool canFire = true;
    public bool isFiring = false;
    public bool hasFired = false;
    public bool cannonShot = false;
    private float firingPower = 16f;
    private float fireTime = 4f;

    // HUD fields
    HUD hud = new HUD();

    // collider fields
    public CircleCollider2D collider;
    public bool ballDestroyed = false;

    // Enemy Field
    [SerializeField]
    Enemy enemy;

    // Cannon Fields
    [SerializeField]
    GameObject cannon;

    // Event fields
    HealthReducedEvent healthReducedEvent = new HealthReducedEvent();
    EnemyRubbleEvent enemyRubbleEvent = new EnemyRubbleEvent();
    PlayerCannonFiredEvent playerCannonFiredEvent = new PlayerCannonFiredEvent();
    // Animation holder fields
    [SerializeField]
    GameObject ExplosionHolder;

    // Damage field
    public int normalDamage = 1;

    // Particle fields
    public ParticleManager particleManager;

    // Respawn timer fields
    [SerializeField]
    GameObject RespawnText;

    // Explosuon animator field
    [SerializeField]
    Animator explosionAnimator;

    // Player Speed fields
    public bool isBoosted = false;
    public bool canSlowDown = false;

    // field to tell when part is damaged
    public bool enemyMastDamaged = false;
    public bool enemyFlagDamaged = false;
    // Start is called before the first frame update
    void Start()
    {
        // get the rigidbody2D component
        rb2d = GetComponent<Rigidbody2D>();
        // get camera component
        cf = GetComponent<CameraFollower>();

        // collision setup
        collider = gameObject.GetComponent<CircleCollider2D>();
        ballDestroyed = false;

        // Save reference to HUD Script
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();

        // get particle script component
        particleManager.GetComponent<ParticleManager>();

        // Set player position to cannons
        transform.position = cannon.transform.position;

        // Set up animator for explosion
        explosionAnimator = ExplosionHolder.GetComponent<Animator>();

        Time.timeScale = 1;
        // Firing setup
        canFire = true;
        hasFired = false;

        // Slowdown/Speedup Set up
        isBoosted = false;
        canSlowDown = true;
        // Add self as an event invoker
        EventManager.AddHealthReducedEventInvoker(this);
        EventManager.AddPlayerCannonFiredEventInvoker(this);

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
        if (hasFired == true)
        {
            // Check if player can slow down
            if (canSlowDown == true)
            {
                // Slowdown funtion
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    slowDownPressed = true;
                }
                else if (Input.GetKeyUp(KeyCode.LeftControl))
                {
                    slowDownPressed = false;
                }
            }
        }
        // Exit Functionality
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Application.Quit();
        }


        if (isFiring == false && canFire == true)
        {
            // fire cannon
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Fire());
                cannonShot = true;
            }
        }

        // If player can fire itself
        if (canFire == true)
        {
            transform.position = cannon.transform.position;
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
            // Check if the ball isn't destroyed
            if (ballDestroyed == false || isBoosted == false)
            {
                accelleration = 20f;
                // move cannon ball freely
                rb2d.MovePosition(rb2d.position + movement * accelleration * Time.deltaTime);
            }

            if (isBoosted == true)
            {
                SpeedUpCannonBall();
            }
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
        hasFired = true;
        float orginalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(transform.localScale.x * firingPower, 0f);

        // Play Sound
        FindObjectOfType<AudioManager>().Play("CannonFire");

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
            if (isBoosted == true)
            {
                // Do damage to ship
                healthReducedEvent.Invoke(3);
            }
            else
            {
                // Do damage to ship
                healthReducedEvent.Invoke(normalDamage);
            }
            // Turn invisible
            ballDestroyed = true;
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
            enemyRubbleEvent.Invoke();
            // spawn animation
            SpawnExplosion();
            // reposition player
            //transform.position = cannon.transform.position;
            //canFire = true;
            //hasFired = false;
            //vc.transform.position = new Vector3(-8.4989f, 0.37f, -21.64309f);
            // If enemy's defeated

        }
        // if player collides with TNT
        if (collision.gameObject.CompareTag("TNT"))
        {
            if (isBoosted == true)
            {
                // Do damage to ship
                healthReducedEvent.Invoke(10);

            }
            else
            {
                // update score
                healthReducedEvent.Invoke(5);
            }
            // Turn invisible
            ballDestroyed = true;
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
            //// reposition player
            //transform.position = cannon.transform.position;
            //canFire = true;
            //hasFired = false;
            //vc.transform.position = new Vector3(-8.4989f, 0.37f, -21.64309f);
            // spawn animation
            SpawnExplosion();

            //if (hud.enemyScore <= 0)
            //{
            //    // make sure the health stays 0
            //    hud.enemyScore = 0;
            //    // Display the win screen
            //    hud.SpawnWinMenu();

            //}
        }
        if (collision.gameObject.CompareTag("MastPiece"))
        {
            // Turn invisible
            ballDestroyed = true;
            gameObject.SetActive(false);
            // Do damage to ship
            healthReducedEvent.Invoke(4);
            // spawn animation
            SpawnExplosion();
            // Activate Sprite animation
            enemyMastDamaged = true;

        }
        if (collision.gameObject.CompareTag("FlagPiece"))
        {
            // Turn invisible
            ballDestroyed = true;
            gameObject.SetActive(false);
            // Do damage to ship
            healthReducedEvent.Invoke(4);
            // spawn animation
            SpawnExplosion();
            // Activate Sprite animation
            enemyFlagDamaged = true;

        }
        // If enemy's defeated
        if (hud.enemyScore <= 0)
        {
            // make sure the health stays 0
            hud.enemyScore = 0;
            // Display the win screen
            hud.SpawnWinMenu();
        }

    }
    /// <summary>
    /// Instantiate Explosion prefab
    /// </summary>
    public void SpawnExplosion()
    {
        Instantiate(ExplosionHolder, transform.position, Quaternion.identity);
        // Play Sound 
        FindObjectOfType<AudioManager>().Play("Explosion");
        //// Check if component isn't empty
        //if (ExplosionHolder != null)
        //{

        //    // make sure componenet is assigned to explosions
        //    if (explosionAnimator != null)
        //    {
        //        Instantiate(ExplosionHolder, transform.position, Quaternion.identity);
        //        bool isActivated = explosionAnimator.GetBool("BallExploded");
        //        explosionAnimator.SetBool("BallExploded", true);
        //        // Destroy explosion prefab
        //        DestroyExplosion(ExplosionHolder);
        //    }
        //}
    }
    /// <summary>
    /// Speed up CannonBall
    /// </summary>
    public void SpeedUpCannonBall()
    {
        // move cannon speed 2x faster
        accelleration *= 2;
        rb2d.MovePosition(rb2d.position + movement * accelleration * Time.deltaTime);
    }
    /// <summary>
    /// Destroy the explosion prefab
    /// </summary>
    public void DestroyExplosion(GameObject explosion)
    {
        DestroyImmediate(explosion, true);
    }
    /// <summary>
    /// Adds listener to the points added event
    /// </summary>
    public void AddHealthReducedEventListener(UnityAction<int> listener)
    {
        healthReducedEvent.AddListener(listener);
    }
    /// <summary>
    /// Add listener to the enemy rubble event
    /// </summary>
    public void AddEnemyRubbleEventListener(UnityAction listener)
    {
        enemyRubbleEvent.AddListener(listener);
    }
    /// <summary>
    /// Add listener to the player cannon fired event
    /// </summary>
    public void AddPlayerCannonFiredEventListener(UnityAction<IEnumerator> listener)
    {
        playerCannonFiredEvent.AddListener(listener);
    }
}
