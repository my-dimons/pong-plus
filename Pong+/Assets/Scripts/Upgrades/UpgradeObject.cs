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
            upgrade.ApplyUpgrade();
            upgrade.AppliedUpgrade();
        });

        gameObject.name = "Upgrade: " + upgrade.upgradeName.ToLower();

        //nameText.text = upgrade.upgradeName;
        descriptionText.text = upgrade.description.ToLower();
        image.sprite = upgrade.image;
    }
}
