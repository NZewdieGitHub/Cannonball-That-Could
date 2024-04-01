using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// A dead zone
/// </summary>
public class DeadZone : MonoBehaviour
{
    // bool fields
    public bool enemyCannonDestroyed = false;

    // Event fields
    HealthReducedEvent healthReducedEvent = new HealthReducedEvent();

    // Hud field
    HUD hud = new HUD();

    // Start is called before the first frame update
    void Start()
    {
        // Save reference to HUD Script
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Destroys game object if it enters the danger zone
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if a game object enters the dead zone
        if (collision.gameObject.CompareTag("EnemyCannonPiece"))
        {
            // destroy the game object that enters dead zone
            Destroy(collision.gameObject);
            enemyCannonDestroyed = true;
            hud.SubtractEnemyPoints(4);
            // If enemy's defeated
            if (hud.enemyScore <= 0)
            {
                // make sure the health stays 0
                hud.enemyScore = 0;
                // Display the win screen
                hud.SpawnWinMenu();
            }
        }
    }
    
}
