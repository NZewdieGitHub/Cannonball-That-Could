using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The speed ring 
/// </summary>
public class SpeedRing : MonoBehaviour
{
    // Player field
    [SerializeField]
    Player player;

    [SerializeField]
    ParticleManager particleManager;
    /// <summary>
    /// Check when player enters the ring
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // make sure only the player exits
        if (collision == player.cC2D)
        {
            if (player.isBoosted == false)
            {
                // Play Sound
                FindObjectOfType<AudioManager>().Play("Boost");
                // instantiate particle effects
                particleManager.SpawnSpeedRing(player.gameObject);
                player.SpeedUpCannonBall();
                player.isBoosted = true;
                // take away slowdown mechaanic
                player.canSlowDown = false;
                // Destroy speed ring
                Destroy(gameObject);
            }

        }
    }

}
