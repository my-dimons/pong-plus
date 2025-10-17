using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool leftGoal;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("PongBall"))
        {
            StartCoroutine(GameManager.RestartRound(3));

            UpgradeManager.Instance.SpawnUpgrades();

            ScoreManager.AddPointsToPaddle(leftGoal, 1);
        }
    }
}
