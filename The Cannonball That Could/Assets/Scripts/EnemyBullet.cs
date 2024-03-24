using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public static float enemySpeed = 12f;
    public Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d.velocity = Vector3.left;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
