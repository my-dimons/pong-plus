using UnityEngine;

[RequireComponent(typeof(Paddle))]
public class PaddleCriticalHit : Ability {
  [Range(0f, 1f)]
  public float criticalHitChance = 0.1f;
  public float criticalHitMultiplier = 2.0f;
  public bool nextHitIsCritical;

  private void OnEnable() => SetBallListeners(true);

  private void OnDisable() => SetBallListeners(false);

  private void SetBallListeners(bool subscribe) {
    foreach (PongBall ball in Utils.GetAllPongBalls()) {
      if (subscribe)
        ball.PaddleBounce += HandleBallPaddleHit;
      else
        ball.PaddleBounce -= HandleBallPaddleHit;
    }
  }

  /// <summary>
  /// If the next hit is critical, apply the critical hit effect to the ball and reset the critical hit state. 
  /// Otherwise, determine if the current hit should be a critical hit based on the criticalHitChance and set the next hit to be critical if it is.
  /// </summary>
  /// <param name="side"></param>
  /// <param name="ball"></param>
  private void HandleBallPaddleHit(PaddleManager.PaddleSides side, PongBall ball) {
    if (side != GetComponent<Paddle>().paddleSide)
      return;

    if (nextHitIsCritical) {
      nextHitIsCritical = false;
      GetComponent<ColorPalette>().overidedColor = GetComponent<Paddle>().paddleSide == PaddleManager.PaddleSides.left
        ? ColorPaletteManager.Instance.theme.leftPaddleColor : ColorPaletteManager.Instance.theme.rightPaddleColor;
      Debug.Log("Critical Hit Resetted");

      ball.criticalHit = true;
      ball.criticalHitMultiplier = criticalHitMultiplier;
      return;
    }

    float randomNum = Random.Range(0f, 1f);

    bool inCriticalHitRange = randomNum <= criticalHitChance;

    if (!ball.criticalHit && inCriticalHitRange) {
      nextHitIsCritical = true;
      GetComponent<ColorPalette>().overidedColor = ColorPaletteManager.Instance.theme.ballCriticalColor;
      Debug.Log("Critical Color: " + ColorPaletteManager.Instance.theme.ballCriticalColor);
      Debug.Log("Next hit is critical!");
    }
  }
}
