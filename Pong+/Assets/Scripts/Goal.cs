using UnityEngine;

public class Goal : MonoBehaviour
{
    public PaddleManager.PaddleSides goalSide;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("PongBall"))
        {
            if (GameManager.Instance.spawnUpgrades)
            {
                UpgradeManager.Instance.SpawnUpgrades(goalSide);
                GameManager.Instance.pauseRound = true;
            }

            StartCoroutine(GameManager.Instance.RestartRound());

            ScoreManager.AddPointsToPaddle(goalSide, 1);
        }
    }
}
