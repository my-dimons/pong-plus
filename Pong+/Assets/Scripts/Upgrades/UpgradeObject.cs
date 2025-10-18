using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeObject : MonoBehaviour
{
    public Upgrade upgrade;

    [Header("Assign in Inspector")]
    public Button button;
    [Space(8)]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    [Space(8)]
    public Image image;
    public Image outline;

    [Header("Other")]
    public PaddleManager.PaddleSides paddleSide;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        SetUI();
    }

    private void SetUI()
    {
        button.onClick.AddListener(() =>
        {
            upgrade.ApplyUpgrade(paddleSide);
            upgrade.AppliedUpgrade();
        });

        SetOutlineColor();

        gameObject.name = "Upgrade: " + upgrade.upgradeName.ToLower();

        //nameText.text = upgrade.upgradeName;
        descriptionText.text = upgrade.description.ToLower();
        image.sprite = upgrade.image;
    }

    private void SetOutlineColor()
    {
        Color color;

        switch (upgrade.upgradeType)
        {
            case UpgradeManager.UpgradeType.normal:
                color = ColorPaletteManager.Instance.GetColorFromPalette(ColorPaletteManager.Instance.theme.normalUpgradeColor);
                break;
            case UpgradeManager.UpgradeType.detrimental:
                color = ColorPaletteManager.Instance.GetColorFromPalette(ColorPaletteManager.Instance.theme.detrimentalUpgradeColor);
                break;
            case UpgradeManager.UpgradeType.ability:
                color = ColorPaletteManager.Instance.GetColorFromPalette(ColorPaletteManager.Instance.theme.abilityUpgradeColor);
                break;
            default:
                color = ColorPaletteManager.Instance.GetColorFromPalette(ColorPaletteManager.ColorPaletteEnum.error);
                break;
        }

        outline.color = color; 
    }

    private void OnEnable()
    {
        ColorPaletteManager.ThemeChanged += SetOutlineColor;
    }
    private void OnDisable()
    {
        ColorPaletteManager.ThemeChanged -= SetOutlineColor;
    }
}
