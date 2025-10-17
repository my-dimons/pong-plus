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
        leftScore.text = ScoreManager.leftPaddleScore.ToString();
        rightScore.text = ScoreManager.rightPaddleScore.ToString();
    }
}
