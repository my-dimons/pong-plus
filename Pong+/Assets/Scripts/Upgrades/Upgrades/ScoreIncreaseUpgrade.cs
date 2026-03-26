using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade/Score Upgrade", order = 0)]
public class ScoreIncreaseUpgrade : Upgrade {
  [Space(10)]
  [Header("Score Upgrade")]
  public int scoreAmount;

  public override void ApplyUpgrade(PaddleManager.PaddleSides side) {
    PaddleManager.PaddleSides otherSide = side == PaddleManager.PaddleSides.left ? PaddleManager.PaddleSides.right : PaddleManager.PaddleSides.left;

    ScoreManager.AddPointsToPaddle(otherSide, scoreAmount);
  }

  public override bool AbleToApplyUpgrade(PaddleManager.PaddleSides side) {
    if (GameManager.Instance.spawnScoreUpgrades)
      return false;

    return true;
  }
}
