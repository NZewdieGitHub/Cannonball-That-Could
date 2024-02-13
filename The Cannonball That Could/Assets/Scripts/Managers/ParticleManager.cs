using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem enemyRubbleParticleEffect;
    
    [SerializeField]
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        Player playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        playerScript.AddEnemyRubbleEventListener(SpawnEnemyRubble);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Spawn Enemy rubble when piece of ship is destroyed
    /// </summary>
    public void SpawnEnemyRubble()
    {
        Instantiate(enemyRubbleParticleEffect, player.transform.position, Quaternion.identity);
    }
}
