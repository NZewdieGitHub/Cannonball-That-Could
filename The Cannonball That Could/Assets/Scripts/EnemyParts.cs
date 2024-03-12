using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enemy Parts
/// </summary>
public class EnemyParts : MonoBehaviour
{
    // Game Object Part Fields
    [SerializeField]
    SpriteRenderer damagedPart;

    [SerializeField]
    Player player;

    private CircleCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.enemyMastDamaged == true)
        {
            // change animation
            ActivateDamageMast();
        }
    }
    /// <summary>
    /// Activates Damaged Enemy Animation
    /// </summary>
    public void ActivateDamageMast()
    {
       // start animation
        if (gameObject != null)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            // make sure componenet is assigned to panel
            if (animator != null)
            {
                bool timerActivated = animator.GetBool("MastDamaged");
                animator.SetBool("MastDamaged", true);
                // turn off collider
                if (collider.enabled == true)
                {
                    collider.enabled = false;
                }
            }
        }
    }

}
