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
            cf.timeRunning = true;
            if (cf.timeRunning) 
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

            // deactivate timer
            cf.timeRunning = false;

            // reset timer count
            cf.exitTime = 3f;
        }
    }
}
