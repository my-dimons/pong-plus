using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeObject : MonoBehaviour {
  public Upgrade upgrade;

  [Header("Assign in Inspector")]
  public Button button;
  [Space(8)]
  public TextMeshProUGUI nameText;
  public TextMeshProUGUI descriptionText;
  [Space(8)]
  public Image image;
  public Image outline;
  [Space(8)]
  public GameObject coolParticles;
  public GameObject claimParticles;
  public Canvas canvas;

  [Header("Other")]
  public PaddleManager.PaddleSides paddleSide;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
    SetUI();
  }

  private void SetUI() {
    button.onClick.AddListener(() => {
      upgrade.ApplyUpgrade(paddleSide);
      upgrade.AppliedUpgrade();
      ClaimParticles();
    });

    SetOutlineColor();

    gameObject.name = "Upgrade: " + upgrade.upgradeName.ToLower();

    //nameText.text = upgrade.upgradeName;
    descriptionText.text = upgrade.description.ToLower();
    image.sprite = upgrade.image;
  }

  private void SetOutlineColor() {
    // color
    outline.color = UpgradeManager.GetColorFromUpgradeType(upgrade.upgradeType);

    // particles
    if (upgrade.upgradeType == UpgradeManager.UpgradeType.uniqueAbility
     || upgrade.upgradeType == UpgradeManager.UpgradeType.ability) {
      if (!coolParticles.TryGetComponent<ParticleSystem>(out var ps)) {
        Debug.LogWarning("Upgrade particles have no ParticleSystem component!");
        return;
      }

      // set color
      var main = ps.main;
      main.startColor = UpgradeManager.GetColorFromUpgradeType(upgrade.upgradeType);

      coolParticles.SetActive(true);
    } else
      coolParticles.SetActive(false);
  }

  public void ClaimParticles() {
    Utils.SpawnBurstParticle(claimParticles, transform.position, Vector3.zero, UpgradeManager.GetColorFromUpgradeType(upgrade.upgradeType));
  }

  private void OnEnable() {
    ColorPaletteManager.ThemeChanged += SetOutlineColor;
  }
  private void OnDisable() {
    ColorPaletteManager.ThemeChanged -= SetOutlineColor;
  }
}
