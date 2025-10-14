using UnityEngine;

public class Paddle : MonoBehaviour
{
    [Header("Player Controlled Paddle")]
    public bool playerControlledPaddle;
    [Tooltip("True = Left Player, False = Right Player (Used for controls)")]
    public bool leftPlayerControls;

    [Header("AI Controlled Paddle")]
    public bool aiControlledPaddle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // check for error between bools
        if (playerControlledPaddle && aiControlledPaddle)
        {
            Debug.LogError(this.gameObject.name + " has both playerControlledPaddle and aiControlledPaddle toggles on!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
