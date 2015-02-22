using UnityEngine;
using System.Collections;

public class Loser : MonoBehaviour {

    public GameObject loserCanvas;

	// Use this for initialization
	void Start () 
    {
        //could be a better way to do this, but having a child object that's a Canvas seems to convert the parent object
        Instantiate(loserCanvas);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
