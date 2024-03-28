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
    [SerializeField]
    HUD hud = new HUD();
    // Start is called before the first frame update
    void Start()
    {
        
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
            Destroy(gameObject);
            Destroy(collision.gameObject);
            SpawnExplosion();
            hud.SubtractPlayerPoints(1);
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
