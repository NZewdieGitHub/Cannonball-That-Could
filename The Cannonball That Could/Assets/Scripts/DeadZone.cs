using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A dead zone
/// </summary>
public class DeadZone : MonoBehaviour
{
    // bool fields
    public bool enemyCannonDestroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Destroys game object if it enters the danger zone
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if a game object enters the dead zone
        if (collision.gameObject.CompareTag("EnemyPiece"))
        {
            // destroy the game object that enters dead zone
            Destroy(collision.gameObject);
            enemyCannonDestroyed = true;
        }
    }

}
