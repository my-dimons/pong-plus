using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade/Paddle Upgrade", order = 0)]
public class PaddleStatUpgrade : Upgrade
{
    [Space(10)]
    [Header("Paddle Stat Changes")]
    public bool changeOtherPaddle;

    [Header("Changes")]
    public float speedChange;
    public float heightChange;

    public override void ApplyUpgrade(PaddleManager.PaddleSides side)
    {
        foreach(Paddle paddle in GetTargetPaddles(side))
        {
            paddle.ChangePaddleSpeed(speedChange);
            paddle.ChangePaddleHeight(heightChange);
        }
    }

    public override bool AbleToApplyUpgrade(PaddleManager.PaddleSides side)
    {
        foreach (Paddle paddle in GetTargetPaddles(side))
        {
            if (IsUpgradeAppliable(paddle))
            {  
                return true;
            }
        }

        return false;
    }

    private bool IsUpgradeAppliable(Paddle paddle)
    {
        return paddle.CanChangePaddleSpeed(speedChange) 
            && paddle.CanChangePaddleHeight(heightChange);
    }

    private bool IsTargetPaddle(Paddle paddle, PaddleManager.PaddleSides side)
    {
        bool sameSide = paddle.paddleSide == side;
        return changeOtherPaddle? !sameSide : sameSide;
    }

    private List<Paddle> GetTargetPaddles(PaddleManager.PaddleSides side)
    {
        List<Paddle> targetPaddles = new List<Paddle>();

        foreach (Paddle paddle in Utils.GetAllPaddles())
        {
            if (IsTargetPaddle(paddle, side))
            {
                targetPaddles.Add(paddle);
            }
        }

        return targetPaddles;
    }
}
