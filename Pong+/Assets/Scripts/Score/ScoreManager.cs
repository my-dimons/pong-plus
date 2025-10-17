using UnityEngine;

public static class ScoreManager
{
    public static int leftPaddleScore;
    public static int rightPaddleScore;

    public static readonly int winningScore = 10;

    public static void ResetScores()
    {
        leftPaddleScore = 0;
        rightPaddleScore = 0;
    }

    public static void AddPointsToPaddle(bool isLeftPaddle, int amount)
    {
        if (isLeftPaddle)
        {
            leftPaddleScore += amount;
        }
        else
        {
            rightPaddleScore += amount;
        }

        // update score text
        ScoreUI scoreUI = GameObject.FindFirstObjectByType<ScoreUI>();

        if (scoreUI != null)
            scoreUI.UpdateScoreText();
        else
            Debug.LogWarning("ScoreUI instance not found in the scene.");
    }

    public static string GetScore(bool isLeftPaddle)
    {
        int score;

        if (isLeftPaddle)
            score = leftPaddleScore;
        else
            score = rightPaddleScore;


        if (score <= 0)
            return "";
        else
            return score.ToString();
    }
}
