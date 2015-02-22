using System;
using UnityEngine;

class PlayerPaddleInputSource : IPaddleInputSource
{
    PaddleDirection IPaddleInputSource.GetMovementDirection(Vector3 ballLocation, Vector3 paddleLocation)
    {
        float vert = Input.GetAxis("Vertical");

        if (vert > 0)
        {
            return PaddleDirection.Down;
        }
        else if (vert < 0)
        {
            return PaddleDirection.Up;
        }
        else
        {
            return PaddleDirection.Stationary;
        }
    }
}

