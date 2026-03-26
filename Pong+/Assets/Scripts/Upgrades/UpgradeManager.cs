using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {
  public static UpgradeManager Instance { get; private set; }

  public AudioClip normalUpgradeSelection;
  public AudioClip specialUpgradeSelection;

  public enum UpgradeType {
    normal, // average upgrade
    detrimental, // bad upgrade (usually hurts other player)
    ability, // upgrade that gives player an abilities stats
    uniqueAbility // special upgrade that gives player a unique ability
  }

  [SerializeField] private List<Upgrade> upgrades = new();

  [Header("Upgrade UI")]

  [SerializeField] private GameObject spawningUpgradeParent;
  [Tooltip("The negative and positive of this value are the bounds that upgrades can spawn at")]
  [SerializeField] private float upgradeSpawningRange;
  [SerializeField] private float upgradeSpawningY;

  [SerializeField] private GameObject upgradePrefab;

  [Space(8)]

  [SerializeField] private int leftSpawningUpgradeAmount = 2;
  [SerializeField] private int rightSpawningUpgradeAmount = 2;

  [Space(4)]

  public int maxUpgradeAmountPerPaddle = 3;

  private List<GameObject> spawnedUpgrades = new();

  private void Awake() {
    // Singleton
    if (Instance != null && Instance != this) {
      Debug.LogWarning("More than 2 instances of the UpgradeManager class were found! Deleting duplicate");
      Destroy(this);
    } else {
      Instance = this;
    }
  }

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
    upgrades = Utils.LoadScriptableObjects<Upgrade>("Upgrades").ToList<Upgrade>();

    foreach (Upgrade upg in upgrades) {
      if (!upg.enabled) {
        upgrades.Remove(upg);
      }
    }
  }

  public void SpawnUpgrades(PaddleManager.PaddleSides paddleSide) {
    List<Upgrade> spawningUpgrades = new();

    int spawningUpgradeAmount = paddleSide == PaddleManager.PaddleSides.left ? leftSpawningUpgradeAmount : rightSpawningUpgradeAmount;
    for (int i = 0; i < spawningUpgradeAmount; i++) {
      spawningUpgrades.Add(GetRandomUpgrade(paddleSide, spawningUpgrades));
    }

    CreateUpgrades(spawningUpgrades, paddleSide);
  }

  private void CreateUpgrades(List<Upgrade> upgs, PaddleManager.PaddleSides paddleSide) {
    for (int i = 0; i < upgs.Count; i++) {
      // spawn upgrade object
      GameObject spawnObj = Instantiate(upgradePrefab, spawningUpgradeParent.transform);

      UpgradeObject spawnObjUpgrade = spawnObj.GetComponent<UpgradeObject>();

      spawnObjUpgrade.upgrade = upgs[i];
      spawnObjUpgrade.paddleSide = paddleSide;

      spawnedUpgrades.Add(spawnObj);
    }
  }

  public Upgrade GetRandomUpgrade(PaddleManager.PaddleSides paddleSide, List<Upgrade> alreadyGottenUpgrades) {
    while (true) {
      // get random upgrade in list
      Upgrade randomUpgrade = upgrades[Random.Range(0, upgrades.Count)];

      // use upgrade weight to check if it should add the upgrade
      float chance = Random.Range(0f, 1f);

      bool ableToApply = randomUpgrade.AbleToApplyUpgrade(paddleSide) && randomUpgrade.enabled;
      bool alreadyGotten = alreadyGottenUpgrades.Contains(randomUpgrade);
      bool inRandomRange = chance <= randomUpgrade.weight;

      if (inRandomRange && !alreadyGotten && ableToApply) {
        return randomUpgrade;
      }
    }
  }

  public void DespawnUpgrades() {
    Camera.main.GetComponent<CameraScript>().ScreenshakeFunction(0.3f);
    if (spawnedUpgrades.Count <= 0)
      return;

    foreach (GameObject upg in spawnedUpgrades) {
      Destroy(upg);
    }

    // resume game
    Debug.Log("Finished picking upgrades, resuming game");
    GameManager.Instance.pauseRound = false;
  }

  public static Color GetColorFromUpgradeType(UpgradeType type) {
    Color color;
    switch (type) {
      case UpgradeManager.UpgradeType.normal:
        color = ColorPaletteManager.Instance.GetColorFromPalette(ColorPaletteManager.Instance.theme.normalUpgradeColor);
        break;
      case UpgradeManager.UpgradeType.detrimental:
        color = ColorPaletteManager.Instance.GetColorFromPalette(ColorPaletteManager.Instance.theme.detrimentalUpgradeColor);
        break;
      case UpgradeManager.UpgradeType.ability:
        color = ColorPaletteManager.Instance.GetColorFromPalette(ColorPaletteManager.Instance.theme.abilityUpgradeColor);
        break;
      case UpgradeManager.UpgradeType.uniqueAbility:
        color = ColorPaletteManager.Instance.GetColorFromPalette(ColorPaletteManager.Instance.theme.uniqueAbilityUpgradeColor);
        break;
      default:
        color = ColorPaletteManager.Instance.GetColorFromPalette(ColorPaletteManager.ColorPaletteEnum.error);
        break;
    }

    return color;
  }

  public void IncreaseAvailableUpgradesAmount(PaddleManager.PaddleSides side, int amount) {
    if (side == PaddleManager.PaddleSides.left)
      leftSpawningUpgradeAmount += amount;
    else
      rightSpawningUpgradeAmount += amount;
  }

  public int GetUpgradeAmount(PaddleManager.PaddleSides side) {
    return (side == PaddleManager.PaddleSides.left) ? leftSpawningUpgradeAmount : rightSpawningUpgradeAmount;
  }
}
