using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Color ColorWithAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

    public static Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
        {
            return color;
        }
        else
        {
            Debug.LogError($"Invalid hex color string: {hex}");
            return Color.white; // Default to white if parsing fails
        }
    }

    public static T[] LoadScriptableObjects<T>(string path) where T : ScriptableObject
    {
        T[] loadedObjects = Resources.LoadAll<T>(path);

        if (loadedObjects.Length <= 0)
        {
            Debug.LogWarning("No commands found in Resources/" + path + " folder.");
            return default;
        }

        foreach (T obj in loadedObjects)
        {
            Debug.Log("Loaded ScriptableObject: " + obj.name);
        }

        return loadedObjects;
    }

    public static void SpawnBurstParticle(GameObject particlePrefab, Vector3 position, Vector3 rotation = default, Color color = default)
    {
        if (rotation == default) rotation = Vector3.zero;

        if (color == default) color = Color.white;

        // Instantiate the particle prefab
        GameObject particleInstance = UnityEngine.Object.Instantiate(particlePrefab, position, Quaternion.Euler(rotation));

        // Get the ParticleSystem component
        if (!particleInstance.TryGetComponent<ParticleSystem>(out var ps))
        {
            Debug.LogWarning("Prefab has no ParticleSystem component!");
            UnityEngine.Object.Destroy(particleInstance);
            return;
        }

        // set color
        var main = ps.main;
        main.startColor = color;

        // Play it (in case it's not already set to play on awake)
        ps.Play();

        // Schedule destruction when it's done
        UnityEngine.Object.Destroy(particleInstance, ps.main.duration + ps.main.startLifetime.constantMax);
    }

    #region Pong Specific
    public static List<PongBall> GetAllPongBalls()
    {
        List<PongBall> balls = new();

        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("PongBall"))
        {
            balls.Add(ball.GetComponent<PongBall>());
        }

        return balls;
    }

    public static List<Paddle> GetSpecificSidePaddles(PaddleManager.PaddleSides side)
    {
        List<Paddle> targetPaddles = new List<Paddle>();

        foreach (Paddle paddleObj in GetAllPaddles())
        {
            if (paddleObj.paddleSide == side)
            {
                targetPaddles.Add(paddleObj);
            }
        }

        return targetPaddles;
    }

    public static List<Paddle> GetAllPaddles()
    {
        List<Paddle> paddles = new List<Paddle>();

        foreach (GameObject paddleObj in GameObject.FindGameObjectsWithTag("Paddle"))
        {
            paddles.Add(paddleObj.GetComponent<Paddle>());
        }

        return paddles;
    }
    #endregion
}