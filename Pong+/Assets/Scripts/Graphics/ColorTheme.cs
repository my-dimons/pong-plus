using UnityEngine;

[CreateAssetMenu(fileName = "Color Theme", order = 0)]
public class ColorTheme : ScriptableObject
{
    public ColorPaletteManager.ColorPaletteEnum backgroundColor;

    [Header("Left Side")]
    public ColorPaletteManager.ColorPaletteEnum leftPaddleColor;
    public ColorPaletteManager.ColorPaletteEnum leftGoalColor;

    [Header("Right Side")]
    public ColorPaletteManager.ColorPaletteEnum rightPaddleColor;
    public ColorPaletteManager.ColorPaletteEnum rightGoalColor;

    [Header("Upgrades")]
    public ColorPaletteManager.ColorPaletteEnum normalUpgradeColor;
    public ColorPaletteManager.ColorPaletteEnum detrimentalUpgradeColor = ColorPaletteManager.ColorPaletteEnum.red;
    public ColorPaletteManager.ColorPaletteEnum abilityUpgradeColor = ColorPaletteManager.ColorPaletteEnum.yellow;

    [Header("Ball")]
    public ColorPaletteManager.ColorPaletteEnum ballColor;
    public ColorPaletteManager.ColorPaletteEnum ballCriticalColor;

    [Header("Msc")]
    public ColorPaletteManager.ColorPaletteEnum wallsColor;
    public ColorPaletteManager.ColorPaletteEnum textColor;
}
