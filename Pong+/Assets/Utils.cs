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

    //public static ScriptableObject[] LoadScriptableObjects(string path)
    //{
    //    string objectsPath = "Upgrades";
    //
    //    ScriptableObject[] loadedObjects = Resources.LoadAll<ScriptableObject>(objectsPath);
    //
    //    if (loadedObjects.Length <= 0)
    //    {
    //        Debug.LogWarning("No commands found in Resources/" + objectsPath + " folder.");
    //        return null;
    //    }
    //
    //    return loadedObjects;
    //}

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
}