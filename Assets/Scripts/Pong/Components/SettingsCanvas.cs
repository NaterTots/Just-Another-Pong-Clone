using UnityEngine;
using System.Collections;

public class SettingsCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("AudioListener volume Before: " + AudioListener.volume.ToString());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnVolumeSlider(float value)
    {
        AudioListener.volume = value;
        Debug.Log("AudioListener volume After: " + value + " , " + AudioListener.volume.ToString());
    }

    public void OnMainMenu()
    {
        Resolver.Instance.GetController<StateEngine>().ChangeGameState(StateEngine.States.MainMenu);
    }
}
