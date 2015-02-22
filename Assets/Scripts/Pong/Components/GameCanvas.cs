using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        Resolver.Instance.GetController<GameStats>().OnPlayerScoreChange += OnPlayerScoreChange;
        Resolver.Instance.GetController<GameStats>().OnOpponentScoreChange += OnOpponentScoreChange;
	}

    void OnDestroy()
    {
        Resolver.Instance.GetController<GameStats>().OnPlayerScoreChange -= OnPlayerScoreChange;
        Resolver.Instance.GetController<GameStats>().OnOpponentScoreChange -= OnOpponentScoreChange;
    }

    void OnPlayerScoreChange(int score)
    {
        transform.FindChild("PlayerScore").GetComponent<Text>().text = score.ToString();
    }

    void OnOpponentScoreChange(int score)
    {
        transform.FindChild("OpponentScore").GetComponent<Text>().text = score.ToString();
    }
}
