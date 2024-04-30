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
    Sprite damagedPart;

    [SerializeField]
    Player player;

    [SerializeField]
    private CapsuleCollider2D MastCollider;

    [SerializeField]
    private BoxCollider2D flagCollider;

    [SerializeField]
    private BoxCollider2D pirateCollider;

    // Damage fields
    public bool pirateBlownUp = false;

    // layer bool
    public bool isSteady;

    // Rigidbody fields
    [SerializeField]
    Rigidbody2D rb2d;
    [SerializeField]
    LayerMask layer;
    // Start is called before the first frame update
    void Start()
    {
        // Make sure items are steady at start of the game
        isSteady = true;
        Physics2D.IgnoreLayerCollision(9, 10, false);
        // Exclude enemy layer from mast layer
        Physics2D.IgnoreLayerCollision(9, 11, false);
        Physics2D.IgnoreLayerCollision(14, 10, false);
        // Exclude enemy layer from flag layer
        Physics2D.IgnoreLayerCollision(9, 12, false);
        Physics2D.IgnoreLayerCollision(14, 11, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.enemyMastDamaged == true)
        {
            // change animation
            ActivateDamageMast();
        }
        if (player.enemyFlagDamaged == true)
        {
            // change animation
            ActivateDamagedFlag();
        }
        else if (pirateBlownUp == true)
        {
            ActivateBlownUpPirate();
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
                bool timerActivated = animator.GetBool("MastDamaged");
                animator.SetBool("MastDamaged", true);
                if (isSteady == false)
                {
                    Physics2D.IgnoreLayerCollision(9, 10, true);
                    Physics2D.IgnoreLayerCollision(14, 10, true);
                }
                
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
                if (isSteady == false)
                {
                    // Exclude enemy layer from flag layer
                    Physics2D.IgnoreLayerCollision(9, 11, true);
                    Physics2D.IgnoreLayerCollision(14, 11, true);
                }
            }
        }
    }
    /// <summary>
    /// Switch pirate spirtes when damaged
    /// </summary>
    public void ActivateBlownUpPirate()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = damagedPart;
        // deactivate pirate collider
        // Exclude enemy layer from flag layer
        // Physics2D.IgnoreLayerCollision(9, 12, true);
        rb2d.excludeLayers = layer;
    }
}
