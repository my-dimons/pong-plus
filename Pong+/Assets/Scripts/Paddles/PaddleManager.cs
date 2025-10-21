using UnityEngine;

public static class PaddleManager
{
    public static readonly float paddleYBounds = 4.2f;

    public static readonly float paddleXOffset = -7;

    public static readonly float minimumPaddleHeight = 0.5f;
    public static readonly float maximumPaddleHeight = 3f;
    public static readonly float minimumPaddleSpeed = 280f;

    public enum PaddleSides
    {
        left,
        right
    }

    public enum ControlTypes
    {
        player,
        ai
    }
}