using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade/Ball Upgrade", order = 0)]
public class BallUpgrade : Upgrade
{
    [Space(10)]

    [Header("Ball Stat Changes Upgrade")]
    public float speedChange;
    public float sizeChange;

    public override void ApplyUpgrade(PaddleManager.PaddleSides side)
    {
        PongBall ball = GameObject.FindGameObjectWithTag("PongBall").GetComponent<PongBall>();

        ball.ChangeSpeed(speedChange);
        ball.ChangeSize(sizeChange);
    }

    public override bool AbleToApplyUpgrade(PaddleManager.PaddleSides side)
    {
        if (IsUpgradeAppliable(GameObject.FindGameObjectWithTag("PongBall").GetComponent<PongBall>()))
        {
            return true;
        }

        return false;
    }
    private bool IsUpgradeAppliable(PongBall ball)
    {
        return ball.CanChangeSpeed(speedChange) && ball.CanChangeSize(sizeChange);
    }
}
