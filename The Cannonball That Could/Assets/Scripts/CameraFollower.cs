using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public float cameraAccelleration = 8f;

    // Player field
    [SerializeField]
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if player presses and holds space
        if (player.slowDownPressed == true)
        {
            // move camera along the x-axis at slower speed
            cameraAccelleration = 2f;
            transform.Translate(Vector2.right * cameraAccelleration * Time.deltaTime);
            Debug.Log("Camera's slowed down");
        }
        else if (player.slowDownPressed == false) 
        {
            // move camera along the x-axis
            transform.Translate(Vector2.right * cameraAccelleration * Time.deltaTime);
        }
        
    }
    /// <summary>
    /// Slows Camera Down
    /// </summary>
    public void SlowDown()
    {
        cameraAccelleration -= -2f;
        
    }
}
