using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public static float enemySpeed = 20f;
    public Rigidbody2D rb2d;
    
    // Animation holder fields
    [SerializeField]
    GameObject ExplosionHolder;

    // hud field
    HUD hud = new HUD();
    GameManager gameManager = new GameManager();
    ParticleManager particleManager = new ParticleManager();
    // Start is called before the first frame update
    void Start()
    {
       hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
        gameManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
        particleManager = GameObject.FindGameObjectWithTag("ParticleManager").GetComponent<ParticleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = Vector3.left * enemySpeed;
    }
    /// <summary>
    /// Handle enemy bullet collision
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerPiece"))
        {
            if (hud.playerScore > 1)
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
                SpawnExplosion();
                particleManager.SpawnPlayerRubble(gameObject);
                hud.SubtractPlayerPoints(1);
            }
            // check if player's health is 0 or lower
            else if (hud.playerScore <= 1)
            {
                gameManager.finalHit = true;
                Destroy(gameObject);
                Destroy(collision.gameObject);
                SpawnExplosion();
                particleManager.SpawnPlayerRubble(gameObject);
                hud.SubtractPlayerPoints(1);
                
            }
        }
    }
    public void SpawnExplosion()
    {
        // Create Explosion
        Instantiate(ExplosionHolder, transform.position, Quaternion.identity);
        // Play Sound 
        FindObjectOfType<AudioManager>().Play("Explosion");

      
    }
}
