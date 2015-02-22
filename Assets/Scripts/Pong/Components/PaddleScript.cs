using UnityEngine;
using System.Collections;

public class PaddleScript : MonoBehaviour 
{

    public float speed = 10.0f;

    public Player controller;
    public Sprite sprite;

    private IPaddleInputSource inputSource;
    private GameDirector director;

	// Use this for initialization
	void Start () 
    {
        GetComponent<SpriteRenderer>().sprite = sprite;

        if (controller == Player.Player)
        {
            inputSource = new PlayerPaddleInputSource();
        }
        else if (controller == Player.Opponent)
        {
            inputSource = new NormalAIPaddleInputSource();
        }
        else //demo mode
        {
            inputSource = new DemoAIPaddleInputSource();
        }

        director = GameObject.FindGameObjectWithTag("Director").GetComponent<GameDirector>();
	}
	
    void FixedUpdate()
    {
        switch (inputSource.GetMovementDirection(director.BallLocation, this.transform.position))
        {
            case PaddleDirection.Up:
                transform.Translate(new Vector2(0.0f, -0.1f));
                break;
            case PaddleDirection.Down:
                transform.Translate(new Vector2(0.0f, 0.1f));
                break;
        }
    }
}
