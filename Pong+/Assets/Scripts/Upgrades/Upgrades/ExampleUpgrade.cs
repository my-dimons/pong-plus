using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade/Example Upgrade", order = 0)]
public class ExampleUpgrade : Upgrade {
  [Space(10)]
  [Header("Example Upgrade")]
  public string example;

  public override void ApplyUpgrade(PaddleManager.PaddleSides side) {

  }

  public override bool AbleToApplyUpgrade(PaddleManager.PaddleSides side) {
    return true;
  }
}
