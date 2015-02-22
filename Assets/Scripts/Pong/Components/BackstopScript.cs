using UnityEngine;
using System.Collections;

public class BackstopScript : MonoBehaviour {

    public Player player;

	// Use this for initialization
	void Start () 
    {
	
	}
	
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Ball")
        {
            Resolver.Instance.GetController<EventController>().FireEvent(GameEvents.GoalScored, new GoalScoredEventArgs(player));
        }
    }
}
