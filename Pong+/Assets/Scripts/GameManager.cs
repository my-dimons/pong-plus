using UnityEngine;
using System.Collections;

public static class GameManager
{
    public static int round;
    
    public static IEnumerator RestartRound(float waitTime)
    {
        round++;

        // possibly better (and faster) way to find objects with type
        GameObject[] paddles = GameObject.FindGameObjectsWithTag("Paddle");
        PongBall ball = GameObject.FindGameObjectWithTag("PongBall").GetComponent<PongBall>();

        ball.ResetPosition();

        yield return new WaitForSeconds(3);

        ball.LaunchBallRandomly();

        // reset paddle pos
        foreach (GameObject paddle in paddles)
        {
            //paddle.GetComponent<Paddle>().OffsetPaddle();
        }
    }

    public static void RestartGame()
    {
        
    }
}
