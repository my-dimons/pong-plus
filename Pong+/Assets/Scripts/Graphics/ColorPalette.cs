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
    public ColorPaletteManager.ColorPaletteEnum overidedColor;

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
        else if (GetComponent<ParticleSystem>())
        {
            if (!this.TryGetComponent<ParticleSystem>(out var ps))
            {
                Debug.LogWarning("Prefab has no ParticleSystem component!");
                return;
            }

            // set color
            var main = ps.main;
            main.startColor = color;
        }
        else if (GetComponent<Camera>())
        {
            Camera.main.backgroundColor = color;
        }
    }

    private void Update()
    {
        UpdateColor();
    }

    void UpdateColor()
    {
        Color color;

        if (useTheme)
        {
            color = ColorPaletteManager.Instance.GetColorFromTheme(currentColorType);
        } else if (!useTheme)
        {
            color = ColorPaletteManager.Instance.GetColorFromPalette(overidedColor);
        } else
        {
            color = ColorPaletteManager.Instance.GetColorFromPalette(ColorPaletteManager.ColorPaletteEnum.error);
        }

        SetColor(Utils.ColorWithAlpha(color, alpha));
    }

    #region Theme Change Event
    /*
    private void OnEnable()
    {
        ColorPaletteManager.ThemeChanged += UpdateColor;
    }

    private void OnDisable()
    {
        ColorPaletteManager.ThemeChanged -= UpdateColor;
    }
    */
    #endregion
}
