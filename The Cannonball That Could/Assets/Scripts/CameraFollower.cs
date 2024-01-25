using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public float cameraAccelleration = 4f;
    [SerializeField]
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // move camera along the x-axis
        transform.Translate(Vector2.right * cameraAccelleration * Time.deltaTime);
    }
    /// <summary>
    /// Slows Camera Down
    /// </summary>
    public void SlowDown()
    {
        cameraAccelleration -= -2f;
        
    }
}
