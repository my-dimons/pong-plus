using UnityEngine;

public static class PaddleManager
{
    public static readonly float paddleYBounds = 4.2f;

    public static readonly float paddleXOffset = -7;

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