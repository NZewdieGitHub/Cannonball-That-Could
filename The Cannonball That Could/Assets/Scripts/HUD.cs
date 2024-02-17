using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    // Text Updating fields (Enemy)
    [SerializeField]
    public TextMeshProUGUI EnemyUI;
    public int enemyScore = 10;

    // Text Updating fields (Player)
    [SerializeField]
    public TextMeshProUGUI PlayerUI;
    public int playerScore = 3;

    // Slider Fields
    public Slider enemyHealthSlider;
    public Slider playerHealthSlider;
    public Gradient enemyGradient;
    public Gradient playerGradient;

    // Panel Field
    [SerializeField]
    GameObject WinPanel;

    // Panel Setup
    [SerializeField]
    GameObject LosePanel;

    // Start is called before the first frame update
    void Start()
    {
        // score setup
        EnemyUI.SetText("Enemy Health: " + enemyScore.ToString());
        // score setup
        PlayerUI.SetText("Player Health: " + playerScore.ToString());

        // Add self as health reduced event listener
        EventManager.AddHealthReducedEventListener(SubtractEnemyPoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Take away points from Enemy
    /// </summary>
    public void SubtractEnemyPoints(int points)
    {
        // updated score
        enemyScore -= points;
        EnemyUI.SetText("Enemy Health: " + enemyScore.ToString());
    }

    /// <summary>
    /// Take away points from Player
    /// </summary>
    public void SubtractPlayerPoints(int points)
    {
        // updated score
        playerScore -= points;
        PlayerUI.SetText("Player Health: " + playerScore.ToString());

        // check if player score is empty
        if (playerScore <= 0)
        {
            playerScore = 0;
            PlayerUI.SetText("Player Health: " + playerScore.ToString());
        }
    }
    /// <summary>
    /// Instantiate Lose Menu
    /// </summary>
    public void SpawnLoseMenu()
    {
        LosePanel.SetActive(true);
        Time.timeScale = 0;
    }
    /// <summary>
    /// Instantiate Win Menu
    /// </summary>
    public void SpawnWinMenu()
    {
        WinPanel.SetActive(true);
        Time.timeScale = 0;
    }
 
}
