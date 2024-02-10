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
    public int enemyScore = 3;

    // Text Updating fields (Player)
    [SerializeField]
    public TextMeshProUGUI PlayerUI;
    public int playerScore = 3;

    // Start is called before the first frame update
    void Start()
    {
        // score setup
        EnemyUI.SetText("Enemy Health: " + enemyScore.ToString());
        // score setup
        PlayerUI.SetText("Player Health: " + playerScore.ToString());
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
}
