using UnityEngine;
using System.Collections;

public class GameDirector : MonoBehaviour 
{
    public GameObject ballPrefab;

    private GameObject theBall;
    private BallScript ballScript;
    public int maxScore;
    public bool demoMode;
    private float preLaunchSleep = 3.0f;
    private AudioSource goalScored;

    private Player _winner;
    public Player Winner
    {
        get
        {
            return _winner;
        }
    }

	// Use this for initialization
	void Start () 
    {
        if (demoMode)
        {
            preLaunchSleep = 0.0f;
        }

        Resolver.Instance.GetController<EventController>().Subscribe(GameEvents.GoalScored, OnGoalScored);
        goalScored = GetComponent<AudioSource>();

        theBall = (GameObject)Instantiate(ballPrefab);
        ballScript = theBall.GetComponent<BallScript>();
        if (demoMode)
        {
            //let's not get out of hand, keep it at a visually appealing speed
            ballScript.maxSpeed = 7.5f;
        }

        StartCoroutine(ResetAndLaunchBall(preLaunchSleep));
	}

    public Vector3 BallLocation
    {
        get
        {
            return theBall.transform.position;
        }
    }

    void OnDestroy()
    {
        Resolver.Instance.GetController<EventController>().UnSubscribe(GameEvents.GoalScored, OnGoalScored);
    }

    public void OnGoalScored(IEventArgs args)
    {
        if (!demoMode)
        {
            goalScored.Play();
        }

        Player goal = (args as GoalScoredEventArgs).Score;

        if (goal == Player.Player)
        {
            GameStats stats = Resolver.Instance.GetController<GameStats>();
            stats.PlayerScore = stats.PlayerScore + 1;
            if (stats.PlayerScore >= maxScore) _winner = Player.Player;
        }
        else
        {
            GameStats stats = Resolver.Instance.GetController<GameStats>();
            stats.OpponentScore = stats.OpponentScore + 1;
            if (stats.OpponentScore >= maxScore) _winner = Player.Opponent;
        }

        if (!demoMode && _winner != Player.Neither)
        {
            Debug.Log("There was a winner!");
            Resolver.Instance.GetController<StateEngine>().ChangeGameState(StateEngine.States.GameOver);
        }
        else
        {
            StartCoroutine(ResetAndLaunchBall(preLaunchSleep));
        }
    }

    IEnumerator ResetAndLaunchBall(float delay)
    {
        ballScript.ResetBall();
        yield return new WaitForSeconds(delay);
        ballScript.LaunchBall();
    }
}
