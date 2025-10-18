using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ColorPaletteManager : MonoBehaviour
{
    public static ColorPaletteManager Instance { get; private set; }
    public ColorTheme theme;
    public List<ColorTheme> availableThemes = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("More than 2 instances of the ColorPaletteManager class were found! Deleting duplicate");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        availableThemes = Utils.LoadScriptableObjects<ColorTheme>("Themes").ToList<ColorTheme>();
    }

    public delegate void OnThemeChanged();
    public static event OnThemeChanged ThemeChanged;

    // COLORS
    static readonly Color green     = Utils.HexToColor("#5fff76");
    static readonly Color white     = Utils.HexToColor("#fcfbed");
    static readonly Color lightGrey = Utils.HexToColor("#dbdbd9");
    static readonly Color black     = Utils.HexToColor("#282b30");
    static readonly Color red       = Utils.HexToColor("#f94a5f");
    static readonly Color blue      = Utils.HexToColor("#4a53f9");
    static readonly Color yellow    = Utils.HexToColor("#f4ef61");

    static readonly Color error     = Utils.HexToColor("#c604d1");

    [HideInInspector] 
    public enum ColorPaletteEnum
    {
        green,
        white,
        grey,
        black,
        red,
        blue,
        lightGrey,
        yellow,
        error
    }

    [HideInInspector] 
    public enum ColorType
    {
        none,
        background,
        leftPaddle,
        leftGoal,
        rightPaddle,
        rightGoal,
        ball,
        walls,
        text
    }

    public Color GetColorFromPalette(ColorPaletteEnum color)
    {
        return color switch
        {
            ColorPaletteEnum.green => green,
            ColorPaletteEnum.white => white,
            ColorPaletteEnum.grey  => lightGrey,
            ColorPaletteEnum.black => black,
            ColorPaletteEnum.red   => red,
            ColorPaletteEnum.blue  => blue,
            ColorPaletteEnum.lightGrey  => lightGrey,
            ColorPaletteEnum.yellow  => yellow,
            _ => error,
        };
    }


    // WARNING, DOES NOT SET THE UPGRADE UI OUTLINE COLOR
    public Color GetColorFromTheme(ColorType type)
    {
        if (theme == null)
        {
            Debug.LogError("No color theme is set in ColorPaletteManager!");
            return error;
        }

        return type switch
        {
            ColorType.background  => GetColorFromPalette(theme.backgroundColor),
            ColorType.leftPaddle  => GetColorFromPalette(theme.leftPaddleColor),
            ColorType.leftGoal    => GetColorFromPalette(theme.leftGoalColor),
            ColorType.rightPaddle => GetColorFromPalette(theme.rightPaddleColor),
            ColorType.rightGoal   => GetColorFromPalette(theme.rightGoalColor),
            ColorType.ball        => GetColorFromPalette(theme.ballColor),
            ColorType.walls       => GetColorFromPalette(theme.wallsColor),
            ColorType.text        => GetColorFromPalette(theme.textColor),
            _ => error,
        };
    }

    public void ChangeTheme(ColorTheme newTheme)
    {
        theme = newTheme;
        ThemeChanged?.Invoke();
    }
}