using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Used for The enemy ship
/// </summary>
public class Enemy : MonoBehaviour
{
    // Collider field
    public BoxCollider2D enemyCollider;

    // Text Updating fields
    [SerializeField]
    public TextMeshProUGUI PlayerUI;
    public int playerScore = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Used for colliding with the enemy ship
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {

        // if player collides with enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // updated score
            playerScore -= 1;
            PlayerUI.SetText("Player Health: " + playerScore.ToString());

            // reposition player
            transform.position = cannon.transform.position;
            canFire = true;
            vc.transform.position = new Vector3(-8.4989f, 0.37f, -21.64309f);
        }
    }
}
