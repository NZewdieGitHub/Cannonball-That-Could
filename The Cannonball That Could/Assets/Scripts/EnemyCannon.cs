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
    public bool canFireAgain = false;

    // Dead Zone Field
    [SerializeField]
    public DeadZone deadZone;
    void Start()
    {
        deadZone.GetComponent<DeadZone>();
    }
    // Update is called once per frame
    void Update()
    {
        // fire only when the player fires
        if (player.cannonShot == true)
        {
            if (deadZone.enemyCannonDestroyed == false) 
            {
                ShootEnemy();
            }
              
           
        }
    }

    public void ShootEnemy()
    {
        if (enemyCannonShot == false)
        {
            // shooting logic
            Instantiate(enemyBullet, firePoint.position, firePoint.rotation);
            enemyCannonShot = true;
        }
    }
}
