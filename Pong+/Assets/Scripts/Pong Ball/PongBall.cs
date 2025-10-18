using UnityEngine;

public class PongBall : MonoBehaviour
{
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
    [SerializeField] private float minimumSpeed = 0.1f;

    private Rigidbody2D rb;

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        // HIT PADDLE
        if (other.gameObject.CompareTag("Paddle"))
        {
            // calculate direction to reflect
            float y = HitFactor(transform.position, other.transform.position, other.collider.bounds.size.y);

            float xDir = Mathf.Sign(transform.position.x - other.transform.position.x);

            Vector2 dir = new Vector2(xDir, y).normalized;
            rb.linearVelocity = dir * speed;

            // add random speed
            speed += extraSpeedOnBounce;
        }
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
        float num = Random.Range(-max, max);

        if (Mathf.Abs(num) < min)
        {
            num = Mathf.Sign(num) * min;
        }

        return num;
    }

    #region Stat Changes
    public void ChangeSpeed(float amount)
    {
        if (!CanChangeSpeed(amount))
            return;

        baseSpeed += Mathf.Clamp(amount, minimumSpeed, Mathf.Infinity);
    }

    public bool CanChangeSpeed(float amount)
    {
        return baseSpeed + amount > minimumSpeed;
    }

    public void ChangeSize(float amount)
    {
        if (!CanChangeSize(amount))
            return;

        float size = Mathf.Clamp(amount, minimumSize, Mathf.Infinity);
        Vector3 newSize = new Vector3(size, size, 0);
        transform.localScale += newSize;
    }

    public bool CanChangeSize(float amount)
    {
        return transform.localScale.x + amount > minimumSize;
    }
    #endregion
}
