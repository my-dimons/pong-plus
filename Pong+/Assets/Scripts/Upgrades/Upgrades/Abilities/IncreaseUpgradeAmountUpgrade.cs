using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade/Ability/Upgrade Upgrade", order = 0)]
public class IncreaseUpgradeAmountUpgrade : Upgrade
{
    [Space(10)]
    [Header("Example Upgrade")]
    public int amountOfNewUpgrades;

    public override void ApplyUpgrade(PaddleManager.PaddleSides side)
    {
        UpgradeManager.Instance.IncreaseAvailableUpgradesAmount(side, amountOfNewUpgrades);
    }

    public override bool AbleToApplyUpgrade(PaddleManager.PaddleSides side)
    {
        if (UpgradeManager.Instance.GetUpgradeAmount(side) <= UpgradeManager.Instance.maxUpgradeAmountPerPaddle)
            return true;

        return false;
    }
}
