using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    // movement fields
    float cameraAccelleration = 3f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().
          AddForce(new Vector2(cameraAccelleration, 0),
          0);
    }
}
