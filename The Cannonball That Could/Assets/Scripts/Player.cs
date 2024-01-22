using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // movement fields
    public Rigidbody2D rb2d;
    public float accelleration = 8f;
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

        // break options
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }
    /// <summary>
    /// Make player move automatically
    /// </summary>
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos = new Vector2(accelleration, 0);

        transform.position = pos;
    }
}
