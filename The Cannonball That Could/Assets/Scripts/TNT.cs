using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// TNT functionality
/// </summary>
public class TNT : MonoBehaviour
{
    // Area of Effect Fields
    public float areaOfEffect;
    public float force;
    public LayerMask layerToHit;

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
    public void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, areaOfEffect, layerToHit);
    }
}
