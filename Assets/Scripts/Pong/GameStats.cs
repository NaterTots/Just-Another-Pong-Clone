using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour, IController
{
    //TODO: this is a ton of boilerplate code for a single stat.  this needs to be refactored
    private int _playerScore;
    public int PlayerScore
    {
        get
        {
            return _playerScore;
        }
        set
        {
            _playerScore = value;
            if (OnPlayerScoreChange != null)
            {
                OnPlayerScoreChange(_playerScore);
            }
        }
    }
    public delegate void PlayerScoreChange(int playerScore);
    public PlayerScoreChange OnPlayerScoreChange;

    private int _opponentScore;
    public int OpponentScore
    {
        get
        {
            return _opponentScore;
        }
        set
        {
            _opponentScore = value;
            if (OnOpponentScoreChange != null)
            {
                OnOpponentScoreChange(_opponentScore);
            }
        }
    }
    public delegate void OpponentScoreChange(int opponentScore);
    public OpponentScoreChange OnOpponentScoreChange;

    // Use this for initialization
    void Start()
    {
        Resolver.Instance.GetController<StateEngine>().SubscribeToStateLoad(StateEngine.States.Playing, ResetStats);
    }

    public void ResetStats(string startState, string endState)
    {
        PlayerScore = 0;
        OpponentScore = 0;
    }

    public void Cleanup()
    {
        
    }
}