using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // movement fields
    public Rigidbody2D rb2d;
    public float accelleration = 3f;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        // get the rigidbody2D component
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // movement input setup
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        
    }
    /// <summary>
    /// Make player move automatically
    /// </summary>
    private void FixedUpdate()
    {
        // break options
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // slowdown cannonball
            accelleration -= 1f;

        }
        else
        {
            GetComponent<Rigidbody2D>().
                AddForce(new Vector2(accelleration, 0),
                0);
        }
    }
}
