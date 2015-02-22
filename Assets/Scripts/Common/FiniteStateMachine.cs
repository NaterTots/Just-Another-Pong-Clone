using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    private string _currentState;
    public string CurrentState
    {
        get
        {
            return _currentState;
        }
    }

    public enum TransitionEventType
    {
        OnAllStarting,
        OnStarting,
        OnAllEnding,
        OnEnding
    };

    public delegate void TransitionEvent(string startState, string endState);

    private struct TransitionEventSubscriptionInfo
    {
        public string State;
        public TransitionEventType TransEventType;
        public TransitionEvent TransEvent;
    }

    private List<string> _statesList = new List<string>();
    private List<TransitionEventSubscriptionInfo> _subscriptionList = new List<TransitionEventSubscriptionInfo>();

    public void AddState(string state)
    {
        if (_statesList.Contains(state))
        {
            Debug.LogWarning("Attempting to add state that already exists: " + state);
        }
        else
        {
            _statesList.Add(state);
        }
    }

    public void SubscribeToAllStateTransitions(TransitionEventType transitionType, TransitionEvent transEvent)
    {
        SubscribeToStateTransition(null, transitionType, transEvent);
    }

    public void SubscribeToStateTransition(string state, TransitionEventType transitionType, TransitionEvent transEvent)
    {
        //first ensure we aren't already subscribed
        foreach (TransitionEventSubscriptionInfo subscriptionInfo in _subscriptionList)
        {
            if (subscriptionInfo.State == state &&
                subscriptionInfo.TransEvent == transEvent &&
                subscriptionInfo.TransEventType == transitionType)
            {
                Debug.LogWarning("Already subscribed to state transition: " + state + " " + transEvent.ToString());
                return;
            }
        }

        _subscriptionList.Add(new TransitionEventSubscriptionInfo()
        {
            State = state,
            TransEvent = transEvent,
            TransEventType = transitionType
        });
    }

    public void TransitionStates(string destinationState)
    {
        //ensure the state exists
        if (!_statesList.Contains(destinationState))
        {
            Debug.LogWarning("Cannot transition to state that doesn't exist: " + destinationState);
            return;
        }

        if (_currentState == destinationState)
        {
            Debug.LogWarning("Cannot transition to the current state.");
            return;
        }

        //fire the OnEnding events
        foreach (TransitionEventSubscriptionInfo subscriptionInfo in _subscriptionList)
        {
            if (subscriptionInfo.TransEventType == TransitionEventType.OnAllEnding ||
                (subscriptionInfo.State == _currentState &&
                 subscriptionInfo.TransEventType == TransitionEventType.OnEnding))
            {
                subscriptionInfo.TransEvent(_currentState, destinationState);
            }
        }

        //transition states
        string oldCurrentState = _currentState;
        _currentState = destinationState;

        //fire the OnStarting events
        foreach (TransitionEventSubscriptionInfo subscriptionInfo in _subscriptionList)
        {
            if (subscriptionInfo.TransEventType == TransitionEventType.OnAllStarting ||
                (subscriptionInfo.State == _currentState &&
                 subscriptionInfo.TransEventType == TransitionEventType.OnStarting))
            {
                subscriptionInfo.TransEvent(oldCurrentState, _currentState);
            }
        }
    }
}