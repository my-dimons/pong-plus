using System;
using UnityEngine;

[RequireComponent(typeof(ColorPalette))]
public class PongBall : MonoBehaviour
{
    #region Variables
    [Header("Start")]

    [Tooltip("Used for the min and max of both the x and y axis on Start() AddForce()")]
    [Range(0, 1)]
    [SerializeField] private float maxInitialForce;
    [Range(0, 1)]
    [SerializeField] private float minInitialForce;

    [Space(5)]

    [Header("Stats")]

    [SerializeField] private float baseSpeed;
    [SerializeField] private float speed;
    [Tooltip("Extra speed added to the ball on each paddle bounce")]
    [SerializeField] private float extraSpeedOnBounce;
    [Space(5)]
    [SerializeField] private float minimumSize;
    [SerializeField] private float minimumBaseSpeed = 0.1f;
    [SerializeField] private float maximumBaseSpeed = 15f;

    [Header("Abilites")]
    public bool criticalHit;

    public static readonly float CRITICAL_HIT_DEFAULT_MULTIPLIER = 1f;
    public float criticalHitMultiplier = 1f;

    [Space(8)]
    [Header("SFX")]
    public AudioClip normalBounceSFX;
    public AudioClip criticalBounceSFX;

    [Header("VFX")]
    public GameObject paddleBounceParticlesPrefab;
    public GameObject wallBounceParticlesPrefab;

    // EVENTS
    public event Action<PaddleManager.PaddleSides, PongBall> PaddleBounce;
    public event Action<PongBall> BallBounce;
    public event Action<PongBall> WallBounce;

    public event Action ChangedSpeed;
    public event Action ChangedSize;

    private Rigidbody2D rb;

    #endregion

    private void Update()
    {
        UpdateBallColor();
    }

    void UpdateBallColor()
    {
        ColorPalette colorPalette = GetComponent<ColorPalette>();
        if (criticalHit)
        {
            colorPalette.overidedColor = ColorPaletteManager.Instance.theme.ballCriticalColor;
        }
        else
        {
            colorPalette.overidedColor = ColorPaletteManager.Instance.theme.ballColor;
        }
    }

    #region Start (Applying Force + Resetting)
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        ResetSpeed();
        ResetPosition();
        LaunchBallRandomly();
    }

    public void ResetPosition()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    public void ResetSpeed()
    {
        speed = baseSpeed;
    }

    public void LaunchBallRandomly()
    {
        float randomX = GenerateRandomForce(minInitialForce, maxInitialForce);
        float randomY = GenerateRandomForce(minInitialForce, maxInitialForce);

        rb.linearVelocity = new Vector2(randomX, randomY) * speed;
    }
    #endregion

    #region On Bounce/Collision
    private void OnCollisionEnter2D(Collision2D other)
    {
        // HIT PADDLE
        if (other.gameObject.CompareTag("Paddle"))
        {
            PaddleBounce?.Invoke(other.gameObject.GetComponent<Paddle>().paddleSide, this);
            BallBounce?.Invoke(this);


            if (criticalHit)
            {
                AudioManager.PlayAudioClip(criticalBounceSFX);
            }
            else
            {
                AudioManager.PlayAudioClip(normalBounceSFX);
            }

            Color color = ColorPaletteManager.Instance.GetColorFromPalette(GetComponent<ColorPalette>().overidedColor);
            Utils.SpawnBurstParticle(paddleBounceParticlesPrefab, transform.position, Quaternion.identity.eulerAngles, color);

            CalculateBounce(other);

            // add random speed
            speed += extraSpeedOnBounce;
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            BallBounce?.Invoke(this);
            WallBounce?.Invoke(this);
        }
    }

    private void CalculateBounce(Collision2D other)
    {
        // calculate direction to reflect
        float y = HitFactor(transform.position, other.transform.position, other.collider.bounds.size.y);

        float xDir = Mathf.Sign(transform.position.x - other.transform.position.x);

        Vector2 dir = new Vector2(xDir, y).normalized;
        rb.linearVelocity = dir * (speed * criticalHitMultiplier);
    }

    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    /// <summary>
    /// Gets a random force from a range, and ensures that the force is not too low (min)
    /// </summary>
    /// <param name="min">Clipping range</param>
    /// <param name="max">Random number input (-max, max)</param>
    /// <returns>A random number between -max and max, if the number is too low (using Math.abs) it sets it to min (negative or positive depending on output)</returns>
    float GenerateRandomForce(float min, float max)
    {
        float num = UnityEngine.Random.Range(-max, max);

        if (Mathf.Abs(num) < min)
        {
            num = Mathf.Sign(num) * min;
        }

        return num;
    }
    #endregion

    #region Stat Changes
    public void ChangeSpeed(float amount)
    {
        if (!CanChangeSpeed(amount))
            return;

        ChangedSpeed?.Invoke();

        baseSpeed = Mathf.Clamp(baseSpeed + amount, minimumBaseSpeed, Mathf.Infinity);
    }

    public bool CanChangeSpeed(float amount)
    {
        return baseSpeed + amount > minimumBaseSpeed && baseSpeed + amount < maximumBaseSpeed;
    }

    public void ChangeSize(float amount)
    {
        if (!CanChangeSize(amount))
            return;

        ChangedSize?.Invoke();

        Vector3 newSize = new Vector3
            (Mathf.Clamp(transform.localScale.x + amount, minimumSize, Mathf.Infinity),
             Mathf.Clamp(transform.localScale.y + amount, minimumSize, Mathf.Infinity), 
             0);

        transform.localScale = newSize;
    }

    public bool CanChangeSize(float amount)
    {
        return ((transform.localScale.x + transform.localScale.y) / 2) + amount > minimumSize;
    }
    #endregion

    #region Abilities

    #region Critical Hit
    void ResetCriticalHit(PaddleManager.PaddleSides side, PongBall ball)
    {   
        if (criticalHit)
        {
            criticalHit = false;
            criticalHitMultiplier = CRITICAL_HIT_DEFAULT_MULTIPLIER;
        }
    }
    void ResetCriticalHit()
    {
        criticalHit = false;
        criticalHitMultiplier = CRITICAL_HIT_DEFAULT_MULTIPLIER;
    }
    #endregion

    #endregion

    #region Add Listeners
    private void OnEnable()
    {
        PaddleBounce += ResetCriticalHit;
        GameManager.StartedRound += ResetCriticalHit;
    }

    private void OnDisable()
    {
        PaddleBounce -= ResetCriticalHit;
        GameManager.StartedRound -= ResetCriticalHit;
    }
    #endregion
}
