using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    [Header("Controls")]
    public PaddleManager.ControlTypes controlType;
    [Tooltip("True = Left Player, False = Right Player (Used for controls)")]
    private PaddleInputActions paddleInputActions;

    [Header("AI Tuning")]
    public bool uselessVariable;

    [Space(10)]
    [Header("Paddle Stats")]
    public PaddleManager.PaddleSides paddleSide;
    public float PaddleSpeed { get; private set; }

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
        if (controlType == PaddleManager.ControlTypes.player)
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
        if (paddleSide == PaddleManager.PaddleSides.left)
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

        if (paddleSide == PaddleManager.PaddleSides.left)
            input = leftInput;
        else
            input = rightInput;

        MovePaddle(PaddleSpeed, input);
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
