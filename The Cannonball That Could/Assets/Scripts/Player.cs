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
    Vector2 vertMovement;
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
    private float fireTime = 2f;
    [SerializeField]
    private TrailRenderer trailRenderer;
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
    //GM field
    public GameManager gameManager;
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
    public bool enemyPirateDamaged = false;

    // Movement limitation field
    public bool canMoveHoriz = false;
    public bool enemySlowedDown = false;
    // To see if player won/lost the game
    public bool playerWon = false;
    public bool playerLost = false;

    // respawn limitation bool 
    public bool lastPlayerHit = false;

    // tells if player is self destructed
    public bool selfDestructed = false;
    
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

        // movement limitation setup
        canMoveHoriz = false;
        
        // Add self as an event invoker
        EventManager.AddHealthReducedEventInvoker(this);
        EventManager.AddPlayerCannonFiredEventInvoker(this);
        EventManager.AddEnemyRubbleEventInvoker(this);
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
        if (canMoveHoriz == true)
        {
            movement.x = Input.GetAxis("Horizontal");
        }
        movement.y = Input.GetAxis("Vertical");
        vertMovement.y = Input.GetAxis("Vertical");
        // restart functionality (for testing purposes)
        if (Input.GetKey(KeyCode.Escape))
        {
            gameManager.PauseGame();
            gameManager.PausePanel.SetActive(true);
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
                    canMoveHoriz = true;
                    enemySlowedDown = true;
                }
                else if (Input.GetKeyUp(KeyCode.LeftControl))
                {
                    slowDownPressed = false;
                    canMoveHoriz = false;
                    enemySlowedDown = false;
                }
            }
        }
       
        if (isFiring == false && canFire == true)
        {
            // fire cannon
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Fire());
                // instantiate dust particles
                particleManager.SpawnDustCloud();
                if (cannonShot == false)
                {
                    cannonShot = true;
                    hud.AddShotCount(1);
                }

            }
        }
        if (playerWon == true)
        {
            transform.position = cannon.transform.position;
        }
        if (playerLost == true)
        {
            transform.position = cannon.transform.position;
        }
        // If player can fire itself
        if (canFire == true)
        {
            transform.position = cannon.transform.position;
        }

        // check if player's ship was destroyed
        if (gameManager.finalHit == true)
        {
            SelfDestruct();
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
                if (canMoveHoriz == true)
                {
                    accelleration = 10f;
                    // move cannon ball freely
                    rb2d.MovePosition(rb2d.position + movement * accelleration * Time.fixedDeltaTime);
                }
                else if (canMoveHoriz == false)
                {
                    accelleration = 20f;
                    vertMovement.y = Input.GetAxis("Vertical");
                    // move cannon ball only vertically
                    rb2d.MovePosition(rb2d.position + (Vector2.right + vertMovement) * accelleration * Time.fixedDeltaTime);
                }


                // check if player lost after going out of bounds
                if (playerLost == true)
                {
                    // make player stand still
                    transform.position = cannon.transform.position;
                }
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
        trailRenderer.emitting = true;
        // Play Sound
        FindObjectOfType<AudioManager>().Play("CannonFire");

        // stop dashing for a short amount of time
        yield return new WaitForSeconds(fireTime);
        rb2d.gravityScale = orginalGravity;
        isFiring = false;
        trailRenderer.emitting = false;
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
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
            particleManager.SpawnEnemyRubble(gameObject);
            // spawn animation
            particleManager.SpawnAngelBall(gameObject);
            SpawnExplosion();
            // reposition player
            //transform.position = cannon.transform.position;
            //canFire = true;
            //hasFired = false;
            //vc.transform.position = new Vector3(-8.4989f, 0.37f, -21.64309f);
            // If enemy's defeated
            if (hud.enemyScore > 0)
            {
                // Turn invisible
                ballDestroyed = true;
                hud.timeRunning = false;
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);
            }
            else if (hud.enemyScore <= 0)
            {
                ballDestroyed = true;
                lastPlayerHit = true;
                gameManager.gameOverTimeRunning = true;
                hud.SpawnWinMenu();
                hud.timeRunning = false;
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);
            }
            // make regular timer invisible
            if (hud.TimeHolder.active == true)
            {
                // make timer invisible
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);
                // shut down timer
                hud.timeRunning = false;
            }
        }
        if (collision.gameObject.CompareTag("Blockage"))
        {
            // Turn invisible
            ballDestroyed = true;
            gameObject.SetActive(false);
            SpawnExplosion();
            // make regular timer invisible
            if (hud.TimeHolder.active == true)
            {
                // make timer invisible
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);
                // shut down timer
                hud.timeRunning = false;
            }
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
                healthReducedEvent.Invoke(8);
            }
            gameObject.SetActive(false);
            // spawn animation
            particleManager.SpawnAngelBall(gameObject);
            SpawnExplosion();
            // Create Explosion Radius
            collision.gameObject.GetComponent<TNT>().Explode();
            Destroy(collision.gameObject);
            if (hud.enemyScore > 0)
            {
                // Turn invisible
                ballDestroyed = true;
                hud.timeRunning = false;
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);
            }
            else if (hud.enemyScore <= 0)
            {
                ballDestroyed = true;
                lastPlayerHit = true;
                gameManager.gameOverTimeRunning = true;
                hud.SpawnWinMenu();
                hud.timeRunning = false;
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);

            }
            // make regular timer invisible
            if (hud.TimeHolder.active == true)
            {
                // make timer invisible
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);
                // shut down timer
                hud.timeRunning = false;
            }
        }
        if (collision.gameObject.CompareTag("MastPiece"))
        {
            
            gameObject.SetActive(false);
            // Do damage to ship
            healthReducedEvent.Invoke(6);
            // spawn animation
            particleManager.SpawnAngelBall(gameObject);
            SpawnExplosion();
            // Activate Sprite animation
            enemyMastDamaged = true;
            // Make Player Phase through layers
            collision.gameObject.GetComponent<EnemyParts>().isSteady = false;
            if (hud.enemyScore > 0)
            {
                // Turn invisible
                ballDestroyed = true;
                hud.timeRunning = false;
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);
            }
            else if (hud.enemyScore <= 0)
            {
                ballDestroyed = true;
                lastPlayerHit = true;
                hud.SpawnWinMenu();
                hud.timeRunning = false;
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);
             
            }
            // make regular timer invisible
            if (hud.TimeHolder.active == true)
            {
                // make timer invisible
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);
                // shut down timer
                hud.timeRunning = false;
            }
        }
        if (collision.gameObject.CompareTag("FlagPiece"))
        {
            gameObject.SetActive(false);
            // Do damage to ship
            healthReducedEvent.Invoke(6);
            // spawn animation
            particleManager.SpawnAngelBall(gameObject);
            SpawnExplosion();
            // Activate Sprite animation
            enemyFlagDamaged = true;
            // Make Player Phase through layers
            collision.gameObject.GetComponent<EnemyParts>().isSteady = false;
            if (hud.enemyScore > 0)
            {
                // Turn invisible
                ballDestroyed = true;
                hud.timeRunning = false;
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);
            }
            else if (hud.enemyScore <= 0)
            {
                ballDestroyed = true;
                lastPlayerHit = true;
                hud.SpawnWinMenu();
                hud.timeRunning = false;
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);

            }
            // make regular timer invisible
            if (hud.TimeHolder.active == true)
            {
                // make timer invisible
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);
                // shut down timer
                hud.timeRunning = false;
            }

        }
        if (collision.gameObject.CompareTag("EnemyPiece"))
        {
            // if player is boosted
            if (isBoosted == true)
            {
                // destroy enemy cannonball
                Destroy(collision.gameObject);
                // spawn animation
                particleManager.SpawnAngelBall(gameObject);
                SpawnExplosion();
            }
            else
            {
                // Turn invisible
                ballDestroyed = true;
                gameObject.SetActive(false);
                // destroy enemy cannonball
                Destroy(collision.gameObject);
                // spawn animation
                particleManager.SpawnAngelBall(gameObject);
                SpawnExplosion();
               
            }
            if (hud.enemyScore <= 0)
            {
                playerWon = true;
                hud.SpawnWinMenu();
            }
            // make regular timer invisible
            if (hud.TimeHolder.active == true)
            {
                // make timer invisible
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);
                // shut down timer
                hud.timeRunning = false;
            }
        }
        if (collision.gameObject.CompareTag("PiratePiece"))
        {
            gameObject.SetActive(false);
            // Do damage to ship
            healthReducedEvent.Invoke(5);
            // spawn animation
            particleManager.SpawnAngelBall(gameObject);
            SpawnExplosion();
            collision.gameObject.GetComponent<EnemyParts>().pirateBlownUp = true;
            
            if (hud.enemyScore > 0)
            {
                // Turn invisible
                ballDestroyed = true;
                hud.timeRunning = false;
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);
            }
            else if (hud.enemyScore <= 0)
            {
                ballDestroyed = true;
                lastPlayerHit = true;
                hud.SpawnWinMenu();
                hud.timeRunning = false;
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);
                collision.gameObject.GetComponent<EnemyParts>().pirateBlownUp = true;
            }
            // make regular timer invisible
            if (hud.TimeHolder.active == true)
            {
                // make timer invisible
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);
                // shut down timer
                hud.timeRunning = false;
            }
        }
        // check for collision with player ship
        if (collision.gameObject.CompareTag("PlayerPiece"))
        {
            // Turn invisible
            ballDestroyed = true;
            gameObject.SetActive(false);
            // Do damage to player ship
            hud.SubtractPlayerPoints(1);
            // destroy enemy cannonball
            Destroy(collision.gameObject);
            // spawn animation
            SpawnExplosion();
            particleManager.SpawnAngelBall(gameObject);
            particleManager.SpawnPlayerRubble(gameObject);
            if (hud.playerScore <= 0)
            {
                gameManager.gameOverTimeRunning = true;
                hud.SpawnLoseMenu();
            }
            // make regular timer invisible
            if (hud.TimeHolder.active == true)
            {
                // make timer invisible
                hud.TextObject.SetActive(false);
                hud.TimeHolder.SetActive(false);
                // shut down timer
                hud.timeRunning = false;
            }
        }

        // check if player's slowed down
        if (enemySlowedDown == true)
        {
            enemySlowedDown = false;
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
        canMoveHoriz = false;
        // move cannon speed 2x faster
        accelleration *= 2;
        rb2d.MovePosition(rb2d.position + (Vector2.right + vertMovement) * accelleration * Time.fixedDeltaTime);
    }
    /// <summary>
    /// Destroy the explosion prefab
    /// </summary>
    public void DestroyExplosion(GameObject explosion)
    {
        DestroyImmediate(explosion, true);
    }
    /// <summary>
    /// Used for when player loses
    /// </summary>
    public void SelfDestruct()
    {
        gameObject.SetActive(false);
        SpawnExplosion();
        particleManager.SpawnAngelBall(gameObject);
        playerLost = true;
        // check if player hasn't won
        if (playerWon == false)
        {
            hud.SpawnLoseMenu();
        }
        cf.cameraAccelleration = 0f;
        vc.transform.Translate(Vector2.right * cf.cameraAccelleration * Time.deltaTime);
        selfDestructed = true;
        hud.RespawnTimeHolder.SetActive(false);
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
