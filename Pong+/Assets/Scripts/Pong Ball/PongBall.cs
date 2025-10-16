using System.Diagnostics;
using UnityEngine;

public class PongBall : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float speed;

    [Tooltip("Used for the min and max of both the x and y axis on Start() AddForce()")]
    [SerializeField] private float maxInitialForce;
    [SerializeField] private float minInitialForce;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float randomX = GenerateRandomForce();
        float randomY = GenerateRandomForce();

        rb.linearVelocity = new Vector2(randomX, randomY) * speed;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // HIT PADDLE
        if (other.gameObject.CompareTag("Paddle"))
        {
            // calculate direction to reflect
            float y = HitFactor(transform.position, other.transform.position, other.collider.bounds.size.y);

            float xDir = Mathf.Sign(transform.position.x - other.transform.position.x);

            Vector2 dir = new Vector2(-xDir, y).normalized;
            rb.linearVelocity = dir * speed;

            // add random speed
            speed += Random.Range();
        }
    }
    
    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    float GenerateRandomForce()
    {
        float num = Random.Range(-maxInitialForce, maxInitialForce);

        if (Mathf.Abs(num) < minInitialForce)
        {
            num = Mathf.Sign(num) * minInitialForce;
        }

        return num;
    }
}
