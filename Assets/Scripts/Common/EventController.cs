using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The Event Controller is used to pass events between two or more objects in the application.  It should be
/// used when a thing happens that multiple classes should be notified about (as an example, colliding with an
/// enemy).  Events can be registered and unregistered on demand and subscribed to and unsubscribed from on demand.
/// When events are fired, they may or may not have a set of arguments that come along with them.
/// </summary>
public class EventController : IController
{
    public bool logWarnings = true;
    public delegate void TriggeredEvent(IEventArgs args);

    private Dictionary<string, List<TriggeredEvent>> _managedEvents = new Dictionary<string, List<TriggeredEvent>>();

    private void Register(string eventName)
    {
        if (!_managedEvents.ContainsKey(eventName))
        {
            _managedEvents.Add(eventName, new List<TriggeredEvent>());
        }
        else if (logWarnings)
        {
            Debug.LogWarning("Attempting to register an event that's already registered: " + eventName);
        }
    }

    private void UnRegister(string eventName)
    {
        if (_managedEvents.ContainsKey(eventName))
        {
            _managedEvents.Remove(eventName);
        }
        else if (logWarnings)
        {
            Debug.LogWarning("Attempting to unregister an event that is not registered: " + eventName);
        }
    }

    public void Subscribe(string eventName, TriggeredEvent trigger)
    {
        if (_managedEvents.ContainsKey(eventName))
        {
            _managedEvents[eventName].Add(trigger);
        }
        else
        {
            Register(eventName);
            Subscribe(eventName, trigger);
        }
    }

    public void UnSubscribe(string eventName, TriggeredEvent trigger)
    {
        if (_managedEvents.ContainsKey(eventName))
        {
            List<TriggeredEvent> events = _managedEvents[eventName];
            for (int i = 0; i < events.Count; i++)
            {
                //NOTE: if a single class is subscribed to an event multiple times (why??) then
                //this will cause all subscriptions to be removed
                if (events[i].Target == trigger.Target)
                {
                    events.RemoveAt(i);
                    break;
                }
            }
        }
        else if (logWarnings)
        {
            Debug.LogWarning("Attempting to unsubscribe to an event that is not registered: " + eventName);
        }
    }

    public void FireEvent(string eventName)
    {
        FireEvent(eventName, EmptyEventArgs);
    }

    public void FireEvent(string eventName, IEventArgs args)
    {
        if (_managedEvents.ContainsKey(eventName))
        {
            TriggeredEvent[] eventArray = _managedEvents[eventName].ToArray();
            foreach (TriggeredEvent trigger in eventArray)
            {
                trigger.Invoke(args);
            }
        }
        else if (logWarnings)
        {
            Debug.LogWarning("Attempting to fire an event that is not registered: " + eventName);
        }
    }

    #region Helper for Empty Event Args

    private class EmptyArgs : IEventArgs
    {
    };

    private static readonly EmptyArgs _emptyArgs = new EmptyArgs();

    public static IEventArgs EmptyEventArgs
    {
        get
        {
            return _emptyArgs;
        }
    }

    #endregion

    #region IController
    public void Cleanup()
    {
    }
    #endregion
}

//When firing an event that needs additional parameters, the parameters object must implement IEventArgs
public interface IEventArgs
{
}
