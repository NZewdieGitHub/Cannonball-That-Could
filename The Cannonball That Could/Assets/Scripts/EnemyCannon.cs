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
    // Enemy Cannon Firing Field
    public bool enemyCannonShot = false;
    private void Start()
    {
         enemyCannonShot = false;
    }
    // Update is called once per frame
    void Update()
    {
        // fire only when the player fires
        if (player.cannonShot == true)
        {
            if (enemyCannonShot == false)
            {
                ShootEnemy();
            }
        }
    }

    public void ShootEnemy()
    {
        // shooting logic
        Instantiate(enemyBullet, firePoint.position, firePoint.rotation);
        enemyCannonShot = true;
    }
}
