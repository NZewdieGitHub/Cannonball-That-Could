using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enemy Parts
/// </summary>
public class EnemyParts : MonoBehaviour
{
    // Game Object holder
    [SerializeField]
    GameObject MastObject;

    // Game Object holder
    [SerializeField]
    GameObject FlagObject;

    // Game Object Part Fields
    [SerializeField]
    SpriteRenderer damagedPart;

    [SerializeField]
    Player player;

    [SerializeField]
    private CapsuleCollider2D MastCollider;

    [SerializeField]
    private BoxCollider2D flagCollider;
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
        else if (player.enemyFlagDamaged == true)
        {
            ActivateDamagedFlag();
        }
        
    }
    /// <summary>
    /// Activates Damaged Mast Animation
    /// </summary>
    public void ActivateDamageMast()
    {
       // start animation
        if (MastObject != null)
        {
            Animator animator = MastObject.GetComponent<Animator>();
            // make sure componenet is assigned to panel
            if (animator != null)
            {
                LayerMask lm;
                bool timerActivated = animator.GetBool("MastDamaged");
                animator.SetBool("MastDamaged", true);
                Physics2D.IgnoreLayerCollision(9,10);
            }
        }
    }
    /// <summary>
    /// Activates Damaged Flag Animation
    /// </summary>
    public void ActivateDamagedFlag()
    {
        // start animation
        if (FlagObject != null)
        {
            Animator animator = FlagObject.GetComponent<Animator>();
            // make sure componenet is assigned to panel
            if (animator != null)
            {
                bool timerActivated = animator.GetBool("FlagDamaged");
                animator.SetBool("FlagDamaged", true);
                // turn off collider
                if (flagCollider.enabled == true)
                {
                    flagCollider.enabled = false;
                }
            }
        }
    }
}
