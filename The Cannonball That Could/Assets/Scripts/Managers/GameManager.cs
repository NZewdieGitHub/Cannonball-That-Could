using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A Game Manager
/// </summary>
public class GameManager : MonoBehaviour
{
    // Player fields
    [SerializeField]
    Player player;

    // Camera field
    [SerializeField]
    GameObject vc;

    // Cannon Fields
    [SerializeField]
    GameObject cannon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Make player respawn after death
    /// </summary>
    public void Respawn()
    {
        // move cannon ball and camera back to starting position
        player.transform.position = cannon.transform.position;
        player.canFire = true;
        player.hasFired = false;
        vc.transform.position = new Vector3(-8.4989f, 0.37f, -21.64309f);
    }
}
