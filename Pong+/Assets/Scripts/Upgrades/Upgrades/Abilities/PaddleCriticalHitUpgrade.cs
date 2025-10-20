using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade/Ability/Critical Hit Ability", order = 0)]
public class PaddleCriticalHitUpgrade : Upgrade
{
    [Space(10)]
    [Header("Critical Hit Ability")]
    public bool addCriticalHitAbility;

    public float criticalHitChance = 0.1f;
    public float criticalHitMultiplier = 1.5f;

    // max/min values
    public static float maxCriticalHitChance = 0.3f;
    public static float maxCriticalHitMultiplier = 2f;
    public override void ApplyUpgrade(PaddleManager.PaddleSides side)
    {
        foreach (Paddle paddle in GetTargetPaddles(side))
        {
            if (paddle.paddleSide == side)
            {
                // apply ability for first time
                if (paddle.GetComponent<PaddleCriticalHit>() == null)
                {
                    PaddleCriticalHit critComponent = paddle.gameObject.AddComponent<PaddleCriticalHit>();

                    critComponent.criticalHitChance = criticalHitChance;
                    critComponent.criticalHitMultiplier = criticalHitMultiplier;
                }
                // apply stat increases
                else
                {
                    PaddleCriticalHit critComponent = paddle.GetComponent<PaddleCriticalHit>();

                    critComponent.criticalHitChance += criticalHitChance;
                    critComponent.criticalHitMultiplier += criticalHitMultiplier;
                }
            }
        }
    }

    public override bool AbleToApplyUpgrade(PaddleManager.PaddleSides side)
    {
        foreach (Paddle paddle in GetTargetPaddles(side))
        {
            if (addCriticalHitAbility && paddle.GetComponent<PaddleCriticalHit>() == null)
            {
                return true;
            }
            else if (!addCriticalHitAbility && paddle.GetComponent<PaddleCriticalHit>() != null)
            {
                // check MAX CRIT CHANCE, and MAX CRIT MULT
                bool upgradeValuesInRange = paddle.GetComponent<PaddleCriticalHit>().criticalHitChance < maxCriticalHitChance ||
                                            paddle.GetComponent<PaddleCriticalHit>().criticalHitMultiplier < maxCriticalHitMultiplier;

                if (upgradeValuesInRange)
                    return true;
            }
        }

        return false;
    }

    private List<Paddle> GetTargetPaddles(PaddleManager.PaddleSides side)
    {
        List<Paddle> targetPaddles = new List<Paddle>();

        foreach (Paddle paddle in Utils.GetSpecificSidePaddles(side))
        {
            targetPaddles.Add(paddle);
        }

        return targetPaddles;
    }
}
