using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    //Pause Panel Fields 
    [SerializeField]
    GameObject PausePanel;
   
    // Start is called before the first frame update
    void Start()
    {
        HUD hudScript = GameObject.FindWithTag("VC").GetComponent<HUD>();
        hudScript.AddRespawnEventListener(Respawn);
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
        vc.transform.position = new Vector3(5.71467f, 0.37f, -21.64309f);
        // respet player speed
        if (player.isBoosted == true)
        {
            player.accelleration = (player.accelleration / 2);
            player.isBoosted = false;
        }
    }
    /// <summary>
    /// Pauses game
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(false);
    }
    /// <summary>
    /// Resumes game
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }
}
