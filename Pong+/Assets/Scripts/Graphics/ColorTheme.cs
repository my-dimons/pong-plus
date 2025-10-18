using UnityEngine;

[CreateAssetMenu(fileName = "Color Theme", order = 0)]
public class ColorTheme : ScriptableObject
{
    public ColorPaletteManager.ColorPaletteEnum backgroundColor;
    public ColorPaletteManager.ColorPaletteEnum leftPaddleColor;
    public ColorPaletteManager.ColorPaletteEnum leftGoalColor;
    public ColorPaletteManager.ColorPaletteEnum rightPaddleColor;
    public ColorPaletteManager.ColorPaletteEnum rightGoalColor;
    public ColorPaletteManager.ColorPaletteEnum ballColor;
    public ColorPaletteManager.ColorPaletteEnum wallsColor;
    public ColorPaletteManager.ColorPaletteEnum textColor;
}
