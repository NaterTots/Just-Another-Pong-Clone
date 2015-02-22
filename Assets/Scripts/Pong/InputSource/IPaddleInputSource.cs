using System;
using UnityEngine;

public enum PaddleDirection
{
    Up,
    Down,
    Stationary
};

interface IPaddleInputSource
{
    PaddleDirection GetMovementDirection(Vector3 ballLocation, Vector3 paddleLocation);
}
