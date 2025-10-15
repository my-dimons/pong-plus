using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerPaddle))]
public class Paddle : MonoBehaviour
{
    [Header("Player Controlled Paddle")]
    public bool playerControlledPaddle;
    [Tooltip("True = Left Player, False = Right Player (Used for controls)")]
    public bool leftPlayerControls;
    private PlayerInput playerInput;
    private PaddleInputActions paddleInputActions;

    [Header("AI Controlled Paddle")]
    public bool aiControlledPaddle;

    // OTHER
    PlayerPaddle playerPaddle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = this.GetComponent<PlayerInput>();
        paddleInputActions = new PaddleInputActions();
        paddleInputActions.LeftPaddle.Enable();
        paddleInputActions.RightPaddle.Enable();
        
        playerPaddle = this.GetComponent<PlayerPaddle>();

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

        if (leftPlayerControls)
        {
            input = 1;
        }
        else
        {
            input = 1;
        }

    }

    void AIControlledPaddle()
    {

    }
}
