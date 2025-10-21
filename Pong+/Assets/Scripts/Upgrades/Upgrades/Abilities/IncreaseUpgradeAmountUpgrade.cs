using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade/Ability/Upgrade Upgrade", order = 0)]
public class IncreaseUpgradeAmountUpgrade : Upgrade
{
    [Space(10)]
    [Header("Example Upgrade")]
    public bool otherSide;
    public int amountOfNewUpgrades;

    public override void ApplyUpgrade(PaddleManager.PaddleSides side)
    {
        UpgradeManager.Instance.IncreaseAvailableUpgradesAmount(GetProperSide(side), amountOfNewUpgrades);
    }

    public override bool AbleToApplyUpgrade(PaddleManager.PaddleSides side)
    {
        if (
            UpgradeManager.Instance.GetUpgradeAmount(GetProperSide(side)) < UpgradeManager.Instance.maxUpgradeAmountPerPaddle
            &&
            UpgradeManager.Instance.GetUpgradeAmount(GetProperSide(side)) > 1)
            return true;

        return false;
    }

    private PaddleManager.PaddleSides GetProperSide(PaddleManager.PaddleSides side)
    {
        if (otherSide && side == PaddleManager.PaddleSides.right)
            return PaddleManager.PaddleSides.left;
        else if (otherSide && side == PaddleManager.PaddleSides.left)
            return PaddleManager.PaddleSides.right;
        else
            return side;
    }
}
