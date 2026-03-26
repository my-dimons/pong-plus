using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour {
  public TextMeshProUGUI leftScore;
  public TextMeshProUGUI rightScore;

  public GameObject rightSelectingText;
  public GameObject leftSelectingText;

  private void Start() {
    UpdateScoreText();
  }

  public void UpdateScoreText() {
    leftScore.text = ScoreManager.GetScore(PaddleManager.PaddleSides.left);
    rightScore.text = ScoreManager.GetScore(PaddleManager.PaddleSides.right);
  }

  public void EnableSelectingText(PaddleManager.PaddleSides paddleSide) {
    if (paddleSide == PaddleManager.PaddleSides.left) {
      leftSelectingText.SetActive(true);
    } else {
      rightSelectingText.SetActive(true);
    }
  }

  public void DisableUpgradeSelectingText() {
    leftSelectingText.SetActive(false);
    rightSelectingText.SetActive(false);
  }
}
