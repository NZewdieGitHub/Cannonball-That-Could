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

    /// <summary>
    /// Check when player enters the ring
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // make sure only the player exits
        if (collision == player.collider)
        {
            if (player.isBoosted == false)
            {
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
