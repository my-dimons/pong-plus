using UnityEngine;

public class Goal : MonoBehaviour
{
    public PaddleManager.PaddleSides goalSide;

    [Header("SFX")]
    public AudioClip goalSFX;

    [Header("VFX")]
    public GameObject goalParticleVFX;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("PongBall"))
        {
            if (GameManager.Instance.spawnUpgrades)
            {
                UpgradeManager.Instance.SpawnUpgrades(goalSide);
                GameManager.Instance.pauseRound = true;
            }

            AudioManager.PlayAudioClip(goalSFX);

            Vector3 particlePos = (other.GetContact(0).point + other.GetContact(1).point) / 2;

            Utils.SpawnBurstParticle(
                goalParticleVFX,
                particlePos,
                color: ColorPaletteManager.Instance.GetColorFromPalette(other.gameObject.GetComponent<ColorPalette>().overidedColor));
                
            Camera.main.GetComponent<CameraScript>().ScreenshakeFunction();

            StartCoroutine(GameManager.Instance.RestartRound());

            ScoreManager.AddPointsToPaddle(goalSide, 1);
        }
    }
}
