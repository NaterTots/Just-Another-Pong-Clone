using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinnerCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        int playerScore = Resolver.Instance.GetController<GameStats>().PlayerScore;
        int opponentScore = Resolver.Instance.GetController<GameStats>().OpponentScore;

        transform.FindChild("FinalScore").GetComponent<Text>().text = playerScore.ToString() + " - " + opponentScore.ToString();

        StartCoroutine(WaitThenPlaySound(1.0f));
	}

    public void OnMainMenu()
    {
        Resolver.Instance.GetController<StateEngine>().ChangeGameState(StateEngine.States.MainMenu);
    }

    IEnumerator WaitThenPlaySound(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<AudioSource>().Play();
    }
}
