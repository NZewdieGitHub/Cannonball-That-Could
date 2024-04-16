using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem enemyRubbleParticleEffect;
    public ParticleSystem playerRubbleParticleEffect;
    public ParticleSystem cloudDustParticleEffect;
    public ParticleSystem speedRingParticleEffect;
    public ParticleSystem angelBallParticleEffect;
    [SerializeField]
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        
        //EventManager.AddEnemyRubbleEventListener(SpawnEnemyRubble);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Spawn Enemy rubble when piece of ship is destroyed
    /// </summary>
    public void SpawnEnemyRubble(GameObject beacon)
    {
        Instantiate(enemyRubbleParticleEffect, beacon.transform.position, Quaternion.identity);
    }
    /// <summary>
    /// Spawn Player rubble when piece of ship is destroyed
    /// </summary>
    public void SpawnPlayerRubble(GameObject beacon)
    {
        Instantiate(playerRubbleParticleEffect, beacon.transform.position, Quaternion.identity);
    }
    /// <summary>
    /// Spawn Dust Clouds when player cannon fires
    /// </summary>
    /// <param name="beacon"></param>
    public void SpawnDustCloud()
    {
        Instantiate(cloudDustParticleEffect, cloudDustParticleEffect.transform.position, cloudDustParticleEffect.transform.rotation);
    }
    /// <summary>
    /// Spawn speed ring particles
    /// </summary>
    public void SpawnSpeedRing(GameObject beacon) 
    {
        Instantiate(speedRingParticleEffect, beacon.transform.position, Quaternion.identity);
    }
    /// <summary>
    /// Spawn Enemy rubble when piece of ship is destroyed
    /// </summary>
    public void SpawnAngelBall(GameObject beacon)
    {
        Instantiate(angelBallParticleEffect, beacon.transform.position, angelBallParticleEffect.transform.rotation);
    }
}
