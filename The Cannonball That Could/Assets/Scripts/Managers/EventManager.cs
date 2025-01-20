using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// An event manager
/// </summary>
public static class EventManager 
{
    // Player fields
    static Player invoker;
    static UnityAction<int> listener;
    static UnityAction<IEnumerator> cannonListener;
    static UnityAction particleListener;

    // Lists of listeners and invokers
    static List<ParticleManager> invokers = new List<ParticleManager>();
    static List<ParticleManager> listeners = new List<ParticleManager>();

    /// <summary>
    /// Add script ass the invoker of the event
    /// </summary>
    /// <param name="script">invoker</param>
    public static void AddHealthReducedEventInvoker(Player script)
    {
        invoker = script;
        if (listener != null)
        {
            invoker.AddHealthReducedEventListener(listener);
        }
    }
    /// <summary>
    /// Adds listener to the events
    /// </summary>
    /// <param name="handler"></param>
    public static void AddHealthReducedEventListener(UnityAction<int> handler)
    {
        listener = handler;
        if (invoker != null)
        {
            invoker.AddHealthReducedEventListener(listener);
        }
    }
    /// <summary>
    /// Add listener to the player cannon fired event
    /// </summary>
    public static void AddPlayerCannonFiredEventInvoker(Player script)
    {
        invoker = script;
        if (cannonListener != null)
        {
            invoker.AddPlayerCannonFiredEventListener(cannonListener);
        }
    }
    /// <summary>
    /// Add listener to the player cannon fired event
    /// </summary>
    public static void AddPlayerCannonFiredEventListener(UnityAction<IEnumerator> handler)
    {
        cannonListener = handler;
        if (invoker != null)
        {
            invoker.AddPlayerCannonFiredEventListener(cannonListener);
        }
    }
    /// <summary>
    /// Add Invoker to the enemy rubble event
    /// </summary>
    public static void AddEnemyRubbleEventInvoker(Player script)
    {
        invoker = script;
        if (particleListener != null)
        {
            invoker.AddEnemyRubbleEventListener(particleListener);
        }
    }
    /// <summary>
    /// Add listener to the enemy rubble event
    /// </summary>
    public static void AddEnemyRubbleEventListener(UnityAction handler)
    {
        particleListener = handler;
        if (invoker != null)
        {
            invoker.AddEnemyRubbleEventListener(particleListener);
        }
    }
}
