using UnityEngine;

class DemoAIPaddleInputSource : IPaddleInputSource
{
    const float stationaryRange = 1.0f;

    float prevBallDistanceFromPaddle;

    PaddleDirection IPaddleInputSource.GetMovementDirection(Vector3 ballLocation, Vector3 paddleLocation)
    {
        float newBallDistanceFromPaddle = Mathf.Abs(ballLocation.x - paddleLocation.x);

        float targetY = 0.0f;

        if (newBallDistanceFromPaddle > prevBallDistanceFromPaddle)
        {
            //go towards the middle of the board (which is 0.0f)
        }
        else
        {
            //get ready for the ball - it's coming
            targetY = ballLocation.y;
        }
        prevBallDistanceFromPaddle = newBallDistanceFromPaddle;

        if (Mathf.Abs(targetY - paddleLocation.y) < stationaryRange)
        {
            return PaddleDirection.Stationary;
        }
        else if (targetY > paddleLocation.y)
        {
            return PaddleDirection.Down;
        }
        else //if (ballLocation.y < paddleLocation.y)
        {
            return PaddleDirection.Up;
        }
    }
}

