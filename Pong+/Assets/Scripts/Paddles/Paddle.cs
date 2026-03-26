using System;
using UnityEngine;

public class Paddle : MonoBehaviour {
  [Header("Controls")]
  public PaddleManager.ControlTypes controlType;
  [Tooltip("True = Left Player, False = Right Player (Used for controls)")]
  private PaddleInputActions paddleInputActions;

  [Header("Paddle Stats")]
  public PaddleManager.PaddleSides paddleSide;

  [Header("AI Tuning")]
  public bool uselessVariable;

  [SerializeField] private float paddleSpeed;

  // EVENTS
  public event Action ChangedSize;
  public event Action ChangedSpeed;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
    paddleInputActions = new PaddleInputActions();
    paddleInputActions.LeftPaddle.Enable();
    paddleInputActions.RightPaddle.Enable();

    OffsetPaddle();
  }

  private void FixedUpdate() {
    if (controlType == PaddleManager.ControlTypes.player) {
      PlayerControlledPaddle();
    } else {
      AIControlledPaddle();
    }
  }

  public void OffsetPaddle() {
    if (paddleSide == PaddleManager.PaddleSides.left) {
      transform.position = new Vector2(PaddleManager.paddleXOffset, 0);
    } else {
      transform.position = new Vector2(-PaddleManager.paddleXOffset, 0);
    }
  }

  #region Movement
  #region Player Controls
  void PlayerControlledPaddle() {
    float input;
    float leftInput = paddleInputActions.LeftPaddle.Input.ReadValue<float>();
    float rightInput = paddleInputActions.RightPaddle.Input.ReadValue<float>();

    if (paddleSide == PaddleManager.PaddleSides.left)
      input = leftInput;
    else
      input = rightInput;

    MovePaddle(paddleSpeed, input);
  }
  #endregion

  #region AI Controls
  void AIControlledPaddle() {

  }
  #endregion

  /// <summary>
  /// Move the object by a speed mutliplied by input by Time.DeltaTime
  /// </summary>
  /// <param name="speed"></param>
  /// <param name="input">Should be a -1 to 1 value</param>
  void MovePaddle(float speed, float input) {
    RepositionPaddleIfNotInBounds();

    float movement = speed * input * Time.deltaTime;
    Rigidbody2D rb = GetComponent<Rigidbody2D>();

    rb.linearVelocity = new Vector2(0, movement);
  }
  #endregion

  #region Checking Bounds
  void RepositionPaddleIfNotInBounds() {
    if (!InArenaBounds()) {
      // upper bounds
      if (AboveUpperBounds()) {
        transform.position = new UnityEngine.Vector2(transform.position.x, PaddleManager.paddleYBounds - HalfYScale());
      }
      // lower bounds
      else if (BelowLowerBounds()) {
        transform.position = new UnityEngine.Vector2(transform.position.x, -PaddleManager.paddleYBounds + HalfYScale());
      }
    }
  }

  bool InArenaBounds() {
    return AboveUpperBounds() || BelowLowerBounds();
  }

  bool AboveUpperBounds() {
    return (transform.position.y + HalfYScale()) > PaddleManager.paddleYBounds;
  }

  bool BelowLowerBounds() {
    return (transform.position.y - HalfYScale()) < -PaddleManager.paddleYBounds;
  }

  float HalfYScale() {
    return transform.localScale.y / 2;
  }

  #endregion

  #region Changing Stats
  public void ChangePaddleSpeed(float newSpeed) {
    if (!CanChangePaddleSpeed(newSpeed))
      return;

    ChangedSpeed?.Invoke();

    paddleSpeed += newSpeed;
  }

  public bool CanChangePaddleSpeed(float speedChange) {
    return paddleSpeed + speedChange >= PaddleManager.minimumPaddleSpeed;
  }

  public void ChangePaddleHeight(float heightChange) {
    if (!CanChangePaddleHeight(heightChange))
      return;

    ChangedSize?.Invoke();

    Vector2 newScale = transform.localScale;
    newScale.y += heightChange;
    transform.localScale = newScale;
  }

  public bool CanChangePaddleHeight(float heightChange) {
    float newHeight = transform.localScale.y + heightChange;
    return newHeight >= PaddleManager.minimumPaddleHeight && newHeight <= PaddleManager.maximumPaddleHeight;
  }

  #endregion
}
