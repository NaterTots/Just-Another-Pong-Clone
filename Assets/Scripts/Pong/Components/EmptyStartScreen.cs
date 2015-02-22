using UnityEngine;
using System.Collections;

public class EmptyStartScreen : MonoBehaviour
{
    public void Awake()
    {
        Resolver.Instance.GetController<StateEngine>().ChangeGameState(StateEngine.States.MainMenu);
    }
}
