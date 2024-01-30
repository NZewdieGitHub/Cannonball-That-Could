using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// TNT functionality
/// </summary>
public class TNT : MonoBehaviour
{
    public BoxCollider2D TNTCollider;
    // Start is called before the first frame update
    void Start()
    {
        TNTCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
