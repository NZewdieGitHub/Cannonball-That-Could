using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // move camera along the x-axis
        transform.Translate(Vector2.right * 2f * Time.deltaTime);
    }
}
