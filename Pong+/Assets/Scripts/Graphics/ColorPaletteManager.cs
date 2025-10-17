using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ColorPaletteManager
{
    // THEME
    public static ColorTheme currentTheme = ColorTheme.normal;

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

    [HideInInspector] 
    public enum ColorTheme
    {
        normal,
        light,
        dark,
        contrast,
        spinblade
    }

    public static Color GetColorFromPalette(ColorPaletteEnum color)
    {
        return color switch
        {
            ColorPaletteEnum.green => green,
            ColorPaletteEnum.white => white,
            ColorPaletteEnum.grey  => lightGrey,
            ColorPaletteEnum.black => black,
            ColorPaletteEnum.red   => red,
            ColorPaletteEnum.blue  => blue,
            _ => error,
        };
    }

    public static Color GetColorFromTheme(ColorType type)
    {
        // normal theme
        if (currentTheme == ColorTheme.normal)
        {
            return type switch
            {
                ColorType.background  => black,
                ColorType.leftPaddle  => green,
                ColorType.leftGoal    => green,
                ColorType.rightPaddle => green,
                ColorType.rightGoal   => green,
                ColorType.ball        => green,
                ColorType.walls       => green,
                ColorType.text        => green,
                _ => error,
            };
        }
        // light theme
        else if (currentTheme == ColorTheme.light)
        {
            return type switch
            {
                ColorType.background  => lightGrey,
                ColorType.leftPaddle  => black,
                ColorType.leftGoal    => black,
                ColorType.rightPaddle => black,
                ColorType.rightGoal   => black,
                ColorType.ball        => black,
                ColorType.walls       => black,
                ColorType.text        => green,
                _ => error,
            };
        }
        else if (currentTheme == ColorTheme.dark)
        {
            return type switch
            {
                ColorType.background  => black,
                ColorType.leftPaddle  => white,
                ColorType.leftGoal    => white,
                ColorType.rightPaddle => white,
                ColorType.rightGoal   => white,
                ColorType.ball        => white,
                ColorType.walls       => white,
                ColorType.text        => green,
                _ => error,
            };
        }
        else if (currentTheme == ColorTheme.contrast)
        {
            return type switch
            {
                ColorType.background  => black,
                ColorType.leftPaddle  => blue,
                ColorType.leftGoal    => blue,
                ColorType.rightPaddle => red,
                ColorType.rightGoal   => red,
                ColorType.ball        => white,
                ColorType.walls       => white,
                ColorType.text        => green,
                _ => error,
            };
        }
        else if (currentTheme == ColorTheme.spinblade)
        {
            return type switch
            {
                ColorType.background  => black,
                ColorType.leftPaddle  => blue,
                ColorType.leftGoal    => blue,
                ColorType.rightPaddle => blue,
                ColorType.rightGoal   => blue,
                ColorType.ball        => red,
                ColorType.walls       => white,
                ColorType.text        => green,
                _ => error,
            };
        }
        else
        {
            return error;
        }
    }

    /*
    private static Color GenerateColorPaletteFromType(ColorType type, Color bg, Color lp, Color lg, Color rp, Color rg, Color ball, Color wall)
    {
        return type switch
        {
            ColorType.background => bg,
            ColorType.leftPaddle => lp,
            ColorType.leftGoal => lg,
            ColorType.rightPaddle => rp,
            ColorType.rightGoal => rg,
            ColorType.ball => ball,
            ColorType.walls => wall,
            _ => error,
        };
    }
    */

    public static void ChangeTheme(ColorTheme newTheme)
    {
        currentTheme = newTheme;
        ThemeChanged?.Invoke();
    }
}