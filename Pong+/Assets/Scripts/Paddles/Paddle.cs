using System.Numerics;
using System.Runtime.CompilerServices;
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
    public bool aiControlledPaddle;

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

        // check for error between bools
        if (playerControlledPaddle && aiControlledPaddle)
        {
            Debug.LogError(this.gameObject.name + " has both playerControlledPaddle and aiControlledPaddle toggles on!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControlledPaddle)
        {
            PlayerControlledPaddle();
        }
        else if (aiControlledPaddle)
        {
            AIControlledPaddle();
        }
    }

    void PlayerControlledPaddle()
    {
        float input;
        float leftInput = paddleInputActions.LeftPaddle.Input.ReadValue<float>();
        float rightInput = paddleInputActions.RightPaddle.Input.ReadValue<float>();

        if (leftPlayerControls)
        {
            input = leftInput;
        }
        else
        {
            input = rightInput;
        }

        if (!(((transform.position.y + GetYScaleHalf()) >= PaddleManager.maxPaddleY) && input < 0) ||
            !(((transform.position.y - GetYScaleHalf()) <= PaddleManager.minPaddleY) && input > 0))
        {
            MovePaddle(paddleSpeed, input);
        }
    }

    void AIControlledPaddle()
    {

    }


    /// <summary>
    /// Move the object by a speed mutliplied by input by Time.DeltaTime
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="input">Should be a -1 to 1 value</param>
    void MovePaddle(float speed, float input)
    {
        RepositionPaddleIfNotInBounds();

        float movement = speed * input * Time.deltaTime;
        this.transform.Translate(new UnityEngine.Vector2(0, movement));
    }

    #region Checking Bounds
    bool InArenaBounds()
    {
        if (AboveUpperBounds() || BelowLowerBounds())
        {
            Debug.Log("Player OUT OF BOUNDS!");
            return false;
        }
        else
        {
            return true;
        }
    }

    bool AboveUpperBounds()
    {
        return (transform.position.y + GetYScaleHalf()) > PaddleManager.maxPaddleY;
    }

    bool BelowLowerBounds()
    {
        return (transform.position.y - GetYScaleHalf()) < -PaddleManager.minPaddleY;
    }
    
    float GetYScaleHalf()
    {
        return transform.localScale.y / 2;
    }

    void RepositionPaddleIfNotInBounds()
    {
        if (!InArenaBounds())
        {
            // upper bounds
            if (AboveUpperBounds())
            {
                transform.position = new UnityEngine.Vector2(transform.position.x, PaddleManager.maxPaddleY - GetYScaleHalf());
            }
            // lower bounds
            else if (BelowLowerBounds())
            {
                transform.position = new UnityEngine.Vector2(transform.position.x, -PaddleManager.minPaddleY + GetYScaleHalf());
            }
        }
    }
    #endregion
}
