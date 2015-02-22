using UnityEngine;
using System.Collections;

public class BaseStateEngine : MonoBehaviour, IController
{
	#region Game State Engine

    private FiniteStateMachine _gameStateEngine = new FiniteStateMachine();
	
	public string CurrentState
	{
		get
		{
			return _gameStateEngine.CurrentState;
		}
	}

	public void ChangeGameState(string newState)
	{
		Debug.Log("ChangeGameState: " + newState);
		
		_gameStateEngine.TransitionStates(newState);
	}

    public void AddGameState(string stateName)
    {
        _gameStateEngine.AddState(stateName);
    }

    public void SubscribeToStateLoad(string stateName, FiniteStateMachine.TransitionEvent transitionEvent)
    {
        _gameStateEngine.SubscribeToStateTransition(stateName, FiniteStateMachine.TransitionEventType.OnStarting, transitionEvent);
    }

    public void SubscribeToStateDestroy(string stateName, FiniteStateMachine.TransitionEvent transitionEvent)
    {
        _gameStateEngine.SubscribeToStateTransition(stateName, FiniteStateMachine.TransitionEventType.OnEnding, transitionEvent);
    }

	#endregion Game State Engine

    #region IController
    public void Cleanup()
    {
    }
    #endregion
}
