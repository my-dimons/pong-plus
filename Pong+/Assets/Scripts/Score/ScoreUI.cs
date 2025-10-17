using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI leftScore;
    public TextMeshProUGUI rightScore;

    private void Start()
    {
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        leftScore.text = ScoreManager.GetScore(false);
        rightScore.text = ScoreManager.GetScore(true);
    }
}
