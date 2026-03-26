using UnityEngine;

public static class ScoreManager {
  public static int leftPaddleScore;
  public static int rightPaddleScore;

  public static readonly int winningScore = 10;

  public static void ResetScores() {
    leftPaddleScore = 0;
    rightPaddleScore = 0;
  }

  public static void AddPointsToPaddle(PaddleManager.PaddleSides paddle, int amount) {
    if (paddle == PaddleManager.PaddleSides.left) {
      leftPaddleScore += amount;
    } else {
      rightPaddleScore += amount;
    }

    // update score text
    ScoreUI scoreUI = GetScoreUI();

    if (scoreUI != null)
      scoreUI.UpdateScoreText();
    else
      Debug.LogWarning("ScoreUI instance not found in the scene.");
  }

  public static string GetScore(PaddleManager.PaddleSides paddleSide) {
    int score;

    if (paddleSide == PaddleManager.PaddleSides.left)
      score = leftPaddleScore;
    else
      score = rightPaddleScore;


    if (score <= 0)
      return "";
    else
      return score.ToString();
  }

  public static ScoreUI GetScoreUI() {
    ScoreUI scoreUI = GameObject.FindFirstObjectByType<ScoreUI>();
    if (scoreUI != null)
      return scoreUI;
    else {
      Debug.LogWarning("ScoreUI instance not found in the scene.");
      return null;
    }
  }
}
