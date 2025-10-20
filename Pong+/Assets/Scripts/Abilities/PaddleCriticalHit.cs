using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Paddle))]
public class PaddleCriticalHit : Ability
{
    [Range(0f, 1f)]
    public float criticalHitChance = 0.1f;
    public float criticalHitMultiplier = 2.0f;

    private void OnEnable() => SetBallListeners(true);

    private void OnDisable() => SetBallListeners(false);

    private void SetBallListeners(bool subscribe)
    {
        foreach (PongBall ball in Utils.GetAllPongBalls())
        {
            if (subscribe)
                ball.PaddleBounce += HandleBallPaddleHit;
            else
                ball.PaddleBounce -= HandleBallPaddleHit;
        }
    }

    private void HandleBallPaddleHit(PaddleManager.PaddleSides side, PongBall ball)
    {
        if (side == GetComponent<Paddle>().paddleSide)
        {
            float randomNum = Random.Range(0f, 1f);

            bool inCriticalHitRange = randomNum <= criticalHitChance;

            if (!ball.criticalHit && inCriticalHitRange)
            {
                Debug.Log("Ball Critical Hit!");

                ball.criticalHit = true;
                ball.criticalHitMultiplier = criticalHitMultiplier;
            }
        }
    }
}
