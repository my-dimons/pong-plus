using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    [Header("Main Upgrade Settings")]
    [Tooltip("Should be 512x512")]
    public Sprite image;

    [Space(5)]
    public string upgradeName;
    public string description;

    [Header("Other Settings")]
    [Range(0, 1)]
    [Tooltip("How often the upgrade appears, 1 = max chance | 0 = no chance")]
    public float weight = 1;
    public bool enabled = true;

    public abstract void ApplyUpgrade();

    public void AppliedUpgrade()
    {
        Debug.Log("Applied Upgrade: " + upgradeName);
        UpgradeManager.Instance.DespawnUpgrades();
    }
}
