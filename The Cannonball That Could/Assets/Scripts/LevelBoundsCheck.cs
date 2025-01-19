using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundsCheck : MonoBehaviour
{
    // HUD fields
    HUD hud = new HUD();

    // Camera Follower fields
    [SerializeField]
    CameraFollower cf;

    [SerializeField]
    GameObject TextObject;
    // Timer parent object holder
    [SerializeField]
    public GameObject TimeHolder;
    // Start is called before the first frame update
    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Give player punishment when entering collider
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // player enters collider
        if (collision.gameObject.CompareTag("Player"))
        {
            // start timer
            hud.timeRunning = true;
            hud.SpawnTimer();
            if (hud.timeRunning) 
            {
                TextObject.SetActive(true);
            }
        }
    }
    /// <summary>
    /// For when the player respawns
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        // player enters collider
        if (collision.gameObject.CompareTag("Player"))
        {
            // Make Object invisible 
            TextObject.SetActive(false);
            TimeHolder.SetActive(false);
            // deactivate timer
            hud.timeRunning = false;
            
            // reset timer count
            hud.exitTime = 3f;

        }
    }
}
