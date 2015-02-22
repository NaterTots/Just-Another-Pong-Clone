using UnityEngine;

class NormalAIPaddleInputSource : IPaddleInputSource
{
    const float stationaryRange = 1.0f;

    private int chanceOfMoving;

    public NormalAIPaddleInputSource()
    {
        chanceOfMoving = Random.Range(75, 85);
    }

    PaddleDirection IPaddleInputSource.GetMovementDirection(Vector3 ballLocation, Vector3 paddleLocation)
    {
        if (Random.Range(0, 100) > chanceOfMoving)
        {
            return PaddleDirection.Stationary;
        }

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

