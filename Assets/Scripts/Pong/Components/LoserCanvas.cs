using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoserCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        int playerScore = Resolver.Instance.GetController<GameStats>().PlayerScore;
        int opponentScore = Resolver.Instance.GetController<GameStats>().OpponentScore;

        transform.FindChild("FinalScore").GetComponent<Text>().text = playerScore.ToString() + " - " + opponentScore.ToString();
	}

    public void OnMainMenu()
    {
        Resolver.Instance.GetController<StateEngine>().ChangeGameState(StateEngine.States.MainMenu);
    }
}
