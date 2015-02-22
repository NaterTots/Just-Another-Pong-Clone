using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour 
{
    public GameObject winner;
    public GameObject loser;

	// Use this for initialization
	void Start () 
    {
        if (Resolver.Instance.GetController<GameStats>().PlayerScore > Resolver.Instance.GetController<GameStats>().OpponentScore)
        {
            Instantiate(winner);
        }
        else
        {
            Instantiate(loser);
        }
	}
}
