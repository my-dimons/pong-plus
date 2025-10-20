using UnityEngine;

public class Goal : MonoBehaviour
{
    public PaddleManager.PaddleSides goalSide;

    [Header("SFX")]
    public AudioClip goalSFX;

    [Header("VFX")]
    public GameObject goalParticleVFX;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("PongBall"))
        {
            if (GameManager.Instance.spawnUpgrades)
            {
                UpgradeManager.Instance.SpawnUpgrades(goalSide);
                GameManager.Instance.pauseRound = true;
            }

            AudioManager.PlayAudioClip(goalSFX);

            Vector2 inbetweenPos = Utils.GetInbetweenPoint(new Vector2(transform.position.x, other.gameObject.transform.position.y), other.gameObject.transform.position);
            Utils.SpawnBurstParticle(
                goalParticleVFX, 
                inbetweenPos, 
                color: ColorPaletteManager.Instance.GetColorFromPalette(other.gameObject.GetComponent<ColorPalette>().overidedColor));

            StartCoroutine(GameManager.Instance.RestartRound());

            ScoreManager.AddPointsToPaddle(goalSide, 1);
        }
    }
}
