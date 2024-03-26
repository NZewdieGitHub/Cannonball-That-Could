using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : MonoBehaviour
{

    // fire point field
    public Transform firePoint;

    // Enemy Prefab Sprite
    public GameObject enemyBullet;
    // Player field 
    public Player player;
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        // fire only when the player fires
        if (player.cannonShot == true)
        {
            ShootEnemy();
            
        }
    }

    public void ShootEnemy()
    {
        // shooting logic
        Instantiate(enemyBullet, firePoint.position, firePoint.rotation);
        player.cannonShot = false;
    }
}
