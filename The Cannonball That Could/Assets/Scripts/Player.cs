using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // movement fields
    public Rigidbody2D rb2d;
    public float accelleration = 1.5f;
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

        GetComponent<Rigidbody2D>().
         AddForce(new Vector2(accelleration, 0),
         0);


    }
    /// <summary>
    /// Make player move automatically
    /// </summary>
    private void FixedUpdate()
    {
     
    }
}
