using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// TNT functionality
/// </summary>
public class TNT : MonoBehaviour
{
    // Area of Effect Fields
    public float areaOfEffect;
    public float force;
    public LayerMask layerToHit;

    [SerializeField]
    ParticleManager pM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// An explosion
    /// </summary>
    public void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, areaOfEffect, layerToHit);
        LayerMask blockLayer = LayerMask.GetMask("Blockage");
        foreach (Collider2D obj in objects) 
        {
            //Vector2 direction = obj.transform.position - transform.position;
            pM.SpawnEnemyRubble(obj.gameObject);
            // chack to ht blockages from all directions
            if (Physics2D.Raycast(transform.position, transform.right, areaOfEffect, blockLayer) || 
                Physics2D.Raycast(transform.position, transform.up, areaOfEffect, blockLayer) 
                || Physics2D.Raycast(transform.position, -transform.right, areaOfEffect, blockLayer) 
                || Physics2D.Raycast(transform.position, -transform.up, areaOfEffect, blockLayer))
            {
                pM.SpawnblockagesRubble(obj.gameObject);
            }
            Destroy(obj.gameObject);
            
        }
    }
    /// <summary>
    /// Create Radius
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaOfEffect);
    }
}
