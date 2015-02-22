using UnityEngine;

class RandomPaddleInputSource : IPaddleInputSource
{
    PaddleDirection IPaddleInputSource.GetMovementDirection(Vector3 ballLocation, Vector3 paddleLocation)
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                return PaddleDirection.Down;
            case 1:
                return PaddleDirection.Stationary;
            case 2:
                return PaddleDirection.Up;
        }
        return PaddleDirection.Stationary;
    }
}

