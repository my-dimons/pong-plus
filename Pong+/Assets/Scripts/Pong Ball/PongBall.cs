using UnityEngine;

public class PongBall : MonoBehaviour
{
    [Tooltip("Used for the min and max of both the x and y axis on Start() AddForce()")]
    [SerializeField] private float _initialForce;
    [HideInInspector] public float variable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        float randomX = Random.Range(-_initialForce, _initialForce);
        float randomY = Random.Range(-_initialForce, _initialForce);
        rb.AddForce(new Vector2(randomX, randomY), ForceMode2D.Impulse);
    }
}
