using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Holds the explosion prefab
/// </summary>
public class ExplosionPlayer : MonoBehaviour
{
    // Animation holder fields
    public GameObject ExplosionHolder;

    [SerializeField]
    public GameObject playerObject;

    // Animator field
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // destroy the game object if the explosion
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Instantiate Explosion prefab
    /// </summary>
    //public void SpawnExplosion()
    //{

    //    // Check if component isn't empty
    //    if (ExplosionHolder != null)
    //    {
    //        Animator animator = ExplosionHolder.GetComponent<Animator>();
    //        // make sure componenet is assigned to explosions
    //        if (animator != null)
    //        {
    //            Instantiate(ExplosionHolder, playerObject.transform.position, Quaternion.identity);
    //            bool isActivated = animator.GetBool("BallExploded");
    //            // inverse animation's current state
    //            animator.SetBool("BallExploded", true);
    //        }
    //    }
    //}
}
