using UnityEngine;
using System.Collections;

public class TitleScreenCanvas : MonoBehaviour 
{
    public void OnPlay()
    {
        Resolver.Instance.GetController<StateEngine>().ChangeGameState(StateEngine.States.Playing);
    }

    public void OnSettings()
    {
        Resolver.Instance.GetController<StateEngine>().ChangeGameState(StateEngine.States.Settings);
    }
}
