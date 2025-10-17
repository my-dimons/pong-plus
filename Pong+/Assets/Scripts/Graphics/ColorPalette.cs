using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorPalette : MonoBehaviour
{
    [Header("Set Theme (Overrides Custom Color)")]
    public bool useTheme;
    public ColorPaletteManager.ColorType currentColorType;

    [Header("Custom Color")]
    public float alpha = 1f;
    public ColorPaletteManager.ColorPaletteEnum color;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateColor();
    }

    void SetColor(Color color)
    {
        if (GetComponent<SpriteRenderer>())
        {
            GetComponent<SpriteRenderer>().color = color;
        }
        else if (GetComponent<TextMeshProUGUI>())
        {
            GetComponent<TextMeshProUGUI>().color = color;
        }
        else if (GetComponent<Image>())
        {
            GetComponent<Image>().color = color;
        }
        else if (GetComponent<Camera>())
        {
            Camera.main.backgroundColor = color;
        }

    }

    void UpdateColor()
    {
        Color color = GetColor();
        if (useTheme)
        {
            color = ColorPaletteManager.GetColorFromTheme(currentColorType);
        }

        SetColor(Utils.ColorWithAlpha(color, alpha));
    }

    Color GetColor()
    {
        return ColorPaletteManager.GetColorFromPalette(color);
    }

    #region Theme Change Event
    private void OnEnable()
    {
        ColorPaletteManager.ThemeChanged += UpdateColor;
    }

    private void OnDisable()
    {
        ColorPaletteManager.ThemeChanged -= UpdateColor;
    }
    #endregion
}
