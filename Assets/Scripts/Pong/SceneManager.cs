using UnityEngine;
using System.Collections;

class SceneManager : MonoBehaviour, IController 
{
    void Awake()
    {
        StateEngine stateEngine = Resolver.Instance.GetController<StateEngine>();
        stateEngine.SubscribeToStateLoad(StateEngine.States.NullState, OnNullStateLoad);
        stateEngine.SubscribeToStateLoad(StateEngine.States.MainMenu, OnMainMenuStateLoad);
        stateEngine.SubscribeToStateLoad(StateEngine.States.Playing, OnPlayingStateLoad);
        stateEngine.SubscribeToStateLoad(StateEngine.States.GameOver, OnGameOverStateLoad);
        stateEngine.SubscribeToStateLoad(StateEngine.States.Settings, OnSettingsStateLoad);
    }

    void Start()
    {
        //no matter what scene we start in, we want to begin with the null state
        Resolver.Instance.GetController<StateEngine>().ChangeGameState(StateEngine.States.NullState);
    }

    void OnNullStateLoad(string startState, string endState)
    {
        Application.LoadLevel("emptystartscene");
    }

    void OnMainMenuStateLoad(string startState, string endState)
    {
        Application.LoadLevel("title");
    }

    void OnPlayingStateLoad(string startState, string endState)
    {
        Application.LoadLevel("game");
    }

    void OnGameOverStateLoad(string startState, string endState)
    {
        Application.LoadLevel("gameover");
    }

    void OnSettingsStateLoad(string stateState, string endState)
    {
        Application.LoadLevel("settings");
    }

    #region IController

    public void Cleanup()
    {

    }

    #endregion IController


}

