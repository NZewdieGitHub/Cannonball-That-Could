using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollower : MonoBehaviour
{
    public float cameraAccelleration = 100f;

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
            cameraAccelleration = 4f;
            // move camera along the x-axis
            transform.Translate(Vector2.right * cameraAccelleration * Time.deltaTime);
            Debug.Log("Camera's sped up");
        }
        
    }
    /// <summary>
    /// Used to determine if player left camera view
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

}
