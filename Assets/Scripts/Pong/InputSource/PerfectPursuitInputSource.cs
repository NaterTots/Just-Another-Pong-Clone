using UnityEngine;

class PerfectPursuitInputSource : IPaddleInputSource
{
    const float stationaryRange = 1.0f;

    PaddleDirection IPaddleInputSource.GetMovementDirection(Vector3 ballLocation, Vector3 paddleLocation)
    {
        if (Mathf.Abs(ballLocation.y - paddleLocation.y) < stationaryRange)
        {
            return PaddleDirection.Stationary;
        }
        else if (ballLocation.y > paddleLocation.y)
        {
            return PaddleDirection.Down;
        }
        else //if (ballLocation.y < paddleLocation.y)
        {
            return PaddleDirection.Up;
        }

    }
}

