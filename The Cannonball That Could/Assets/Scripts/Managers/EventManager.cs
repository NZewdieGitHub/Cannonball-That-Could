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
}
