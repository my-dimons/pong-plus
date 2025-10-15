using System.Diagnostics;
using UnityEngine;

public class PongBall : MonoBehaviour
{
    [Tooltip("Used for the min and max of both the x and y axis on Start() AddForce()")]
    [SerializeField] private float maxInitialForce;
    [SerializeField] private float minInitialForce;
    public float speed;
    [HideInInspector] public float variable;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        rb.linearVelocity = Vector2.left * speed;

        float randomX = GenerateRandomForce();
        float randomY = GenerateRandomForce();

        //rb.AddForce(new Vector2(randomX, randomY), ForceMode2D.Impulse);
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            float y = HitFactor(transform.position, other.transform.position, other.collider.bounds.size.y);

            float xDir = Mathf.Sign(transform.position.x - other.transform.position.x);

            Vector2 dir = new Vector2(-xDir, y).normalized;
            rb.linearVelocity = dir * speed;
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
