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
}