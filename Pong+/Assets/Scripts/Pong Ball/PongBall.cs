using UnityEngine;

public class PongBall : MonoBehaviour
{
    [Tooltip("Used for the min and max of both the x and y axis on Start() AddForce()")]
    [SerializeField] private float maxInitialForce;
    [SerializeField] private float minInitialForce;

    [HideInInspector] public float variable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        float randomX = GenerateRandomForce();
        float randomY = GenerateRandomForce();

        rb.AddForce(new Vector2(randomX, randomY), ForceMode2D.Impulse);
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
