using UnityEngine;
using System.Collections;

public class StateEngine : BaseStateEngine
{
    public enum States
    {
        NullState,
        MainMenu,
        Playing,
        GameOver,
        Settings
    }

    #region MonoBehaviour

    void Awake()
    {
        AddGameState(States.NullState.ToString());
        AddGameState(States.MainMenu.ToString());
        AddGameState(States.Playing.ToString());
        AddGameState(States.GameOver.ToString());
        AddGameState(States.Settings.ToString());
    }

    #endregion MonoBehaviour


    #region Game State Engine

    public void ChangeGameState(States state)
    {
        ChangeGameState(state.ToString());
    }

    public void SubscribeToStateLoad(States state, FiniteStateMachine.TransitionEvent transitionEvent)
    {
        SubscribeToStateLoad(state.ToString(), transitionEvent);
    }

    public void SubscribeToStateDestroy(States state, FiniteStateMachine.TransitionEvent transitionEvent)
    {
        SubscribeToStateDestroy(state.ToString(), transitionEvent);
    }

    #endregion Game State Engine
}
