using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.ObjectModel;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action StartedRound;

    [Header("Spawning Ball")]
    public GameObject predictionArrow;

    [Header("Upgrade Settings")]
    public bool spawnUpgrades = true;

    [Space(8)]

    [SerializeField] private int round;
    public bool pauseRound; // used for restarting round and upgrades
    bool resetingBall;
    public int pauseTimeAfterUpgrades = 1;

    [Header("SFX")]
    public AudioClip startRoundSFX;

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

    void Start()
    {
        StartCoroutine(RestartRound());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && !resetingBall)
        {
            StartCoroutine(ResetBall());
            Debug.Log("Restarting round via input!");
        }

    }

    /// <summary>
    /// Resets ball position
    /// </summary>
    public IEnumerator ResetBall()
    {
        resetingBall = true;

        PongBall ball = GameObject.FindGameObjectWithTag("PongBall").GetComponent<PongBall>();

        ball.ResetPosition();
        ball.GenerateRandomForce(ball.minInitialForce, ball.maxInitialForce);
        predictionArrow.SetActive(true);
        PositionBallPredictionArrow(ball.randomForceDirection);

        yield return new WaitForSeconds(pauseTimeAfterUpgrades); // wait before launching ball so player can prepare

        Debug.Log("started round");

        AudioManager.PlayAudioClip(startRoundSFX);
        predictionArrow.SetActive(false);

        ball.ResetSpeed();
        ball.LaunchBall();

        resetingBall = false;
    }

    public IEnumerator RestartRound()
    {
        resetingBall = true;

        round++;
        StartedRound?.Invoke();

        GameObject[] paddles = GameObject.FindGameObjectsWithTag("Paddle");
        PongBall ball = GameObject.FindGameObjectWithTag("PongBall").GetComponent<PongBall>();

        ball.ResetPosition();
        ball.ResetCriticalHit();
        ball.GenerateRandomForce(ball.minInitialForce, ball.maxInitialForce);
        predictionArrow.SetActive(true);
        PositionBallPredictionArrow(ball.randomForceDirection);

        while (pauseRound)
        {
            ball.ResetPosition();
            yield return null;
        }

        yield return new WaitForSeconds(pauseTimeAfterUpgrades); // wait before launching ball so player can prepare

        Debug.Log("started round");

        AudioManager.PlayAudioClip(startRoundSFX);
        predictionArrow.SetActive(false);

        ball.ResetSpeed();
        ball.LaunchBall();
        /* reset paddle pos
        foreach (GameObject paddle in paddles)
        {
            paddle.GetComponent<Paddle>().OffsetPaddle();
        }
        */

        resetingBall = false;
    }

    private void PositionBallPredictionArrow(Vector2 forceDirection)
    {
        float rad = Mathf.Atan2(-forceDirection.y, -forceDirection.x);
        float angle = rad * (180 / (float) Math.PI);

        predictionArrow.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public static void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
