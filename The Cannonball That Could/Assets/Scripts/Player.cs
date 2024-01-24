using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // movement fields
    public Rigidbody2D rb2d;
    public float accelleration = 4f;
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
        // move cannon ball freely
        rb2d.MovePosition(rb2d.position + movement * accelleration * Time.deltaTime);
    }
}
