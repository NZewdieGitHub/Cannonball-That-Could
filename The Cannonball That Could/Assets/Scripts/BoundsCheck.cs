using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Used to keep gameobject on screen
/// </summary>
public class BoundsCheck : MonoBehaviour
{
   
    // Collider fields
    CircleCollider2D collider;
    public float camWidth;
    public float camHeight;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
