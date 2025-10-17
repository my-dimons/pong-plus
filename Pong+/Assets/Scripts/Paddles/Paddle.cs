using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    [Header("Player Controlled Paddle")]
    public bool playerControlledPaddle;
    [Tooltip("True = Left Player, False = Right Player (Used for controls)")]
    private PaddleInputActions paddleInputActions;

    [Header("AI Controlled Paddle")]
    public bool uselessVariable;

    [Space(10)]
    [Header("Paddle Stats")]
    public bool leftPaddle;
    [SerializeField] private float paddleSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        paddleInputActions = new PaddleInputActions();
        paddleInputActions.LeftPaddle.Enable();
        paddleInputActions.RightPaddle.Enable();

        OffsetPaddle();
    }

    private void FixedUpdate()
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

    public void OffsetPaddle()
    {
        if (leftPaddle)
        {
            transform.position = new Vector2(PaddleManager.paddleXOffset, 0);
        } else
        {
            transform.position = new Vector2(-PaddleManager.paddleXOffset, 0);
        }
    }

    #region Player Controls
    void PlayerControlledPaddle()
    {
        float input;
        float leftInput = paddleInputActions.LeftPaddle.Input.ReadValue<float>();
        float rightInput = paddleInputActions.RightPaddle.Input.ReadValue<float>();

        if (leftPaddle)
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
                transform.position = new UnityEngine.Vector2(transform.position.x, PaddleManager.paddleYBounds - HalfYScale());
            }
            // lower bounds
            else if (BelowLowerBounds())
            {
                transform.position = new UnityEngine.Vector2(transform.position.x, -PaddleManager.paddleYBounds + HalfYScale());
            }
        }
    }

    bool InArenaBounds()
    {
        return AboveUpperBounds() || BelowLowerBounds();
    }

    bool AboveUpperBounds()
    {
        return (transform.position.y + HalfYScale()) > PaddleManager.paddleYBounds;
    }

    bool BelowLowerBounds()
    {
        return (transform.position.y - HalfYScale()) < -PaddleManager.paddleYBounds;
    }
    
    float HalfYScale()
    {
        return transform.localScale.y / 2;
    }

    #endregion
}
