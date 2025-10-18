using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Upgrade Settings")]
    public bool spawnUpgrades = true;

    [Space(8)]

    [SerializeField] private int round;
    public bool pauseRound; // used for restarting round and upgrades

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("More than 2 instances of the GameManager class were found! Deleting duplicate");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public IEnumerator RestartRound()
    {
        round++;

        // possibly better (and faster) way to find objects with type
        GameObject[] paddles = GameObject.FindGameObjectsWithTag("Paddle");
        PongBall ball = GameObject.FindGameObjectWithTag("PongBall").GetComponent<PongBall>();

        ball.ResetPosition();

        while (pauseRound)
        {
            yield return null;
        }

        ball.LaunchBallRandomly();

        /* reset paddle pos
        foreach (GameObject paddle in paddles)
        {
            paddle.GetComponent<Paddle>().OffsetPaddle();
        }
        */
    }

    public static void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
