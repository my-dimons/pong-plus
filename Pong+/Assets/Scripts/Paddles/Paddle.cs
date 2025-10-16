using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    [Header("Player Controlled Paddle")]
    public bool playerControlledPaddle;
    [Tooltip("True = Left Player, False = Right Player (Used for controls)")]
    public bool leftPlayerControls;
    private PaddleInputActions paddleInputActions;

    [Header("AI Controlled Paddle")]
    public bool variableUseless;

    [Space(10)]
    [Header("Paddle Stats")]
    [SerializeField] private float paddleSpeed;
    public GameObject test;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        paddleInputActions = new PaddleInputActions();
        paddleInputActions.LeftPaddle.Enable();
        paddleInputActions.RightPaddle.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControlledPaddle)
        {
            PlayerControlledPaddle();
        }
        else
        {
            AIControlledPaddle();
        }
    }

    #region Player Controls
    void PlayerControlledPaddle()
    {
        float input;
        float leftInput = paddleInputActions.LeftPaddle.Input.ReadValue<float>();
        float rightInput = paddleInputActions.RightPaddle.Input.ReadValue<float>();

        if (leftPlayerControls)
            input = leftInput;
        else
            input = rightInput;

        MovePaddle(paddleSpeed, input);
    }
    #endregion

    #region AI Controls
    void AIControlledPaddle()
    {

    }
    #endregion


    /// <summary>
    /// Move the object by a speed mutliplied by input by Time.DeltaTime
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="input">Should be a -1 to 1 value</param>
    void MovePaddle(float speed, float input)
    {
        RepositionPaddleIfNotInBounds();

        float movement = speed * input * Time.deltaTime;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.linearVelocity = new Vector2(0, movement);
    }

    #region Checking Bounds
    void RepositionPaddleIfNotInBounds()
    {
        if (!InArenaBounds())
        {
            // upper bounds
            if (AboveUpperBounds())
            {
                transform.position = new UnityEngine.Vector2(transform.position.x, PaddleManager.paddleBounds - HalfYScale());
            }
            // lower bounds
            else if (BelowLowerBounds())
            {
                transform.position = new UnityEngine.Vector2(transform.position.x, -PaddleManager.paddleBounds + HalfYScale());
            }
        }
    }

    bool InArenaBounds()
    {
        return AboveUpperBounds() || BelowLowerBounds();
    }

    bool AboveUpperBounds()
    {
        return (transform.position.y + HalfYScale()) > PaddleManager.paddleBounds;
    }

    bool BelowLowerBounds()
    {
        return (transform.position.y - HalfYScale()) < -PaddleManager.paddleBounds;
    }
    
    float HalfYScale()
    {
        return transform.localScale.y / 2;
    }

    #endregion
}
