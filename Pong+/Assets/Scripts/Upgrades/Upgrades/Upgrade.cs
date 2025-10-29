using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    [Header("Main Upgrade Settings")]
    [Tooltip("Should be 512x512")]
    public Sprite image;

    [Space(5)]
    public string upgradeName;
    [TextArea(2, 5)]
    public string description;

    [Header("Spawn Settings")]  
    public UpgradeManager.UpgradeType upgradeType = UpgradeManager.UpgradeType.normal;

    [Range(0, 1)]
    [Tooltip("How often the upgrade appears, 1 = max chance | 0 = no chance")]
    public float weight = 1;
    public bool enabled = true;

    public abstract void ApplyUpgrade(PaddleManager.PaddleSides side);
    public abstract bool AbleToApplyUpgrade(PaddleManager.PaddleSides side);

    public virtual void AppliedUpgrade()
    {
        Debug.Log("Applied Upgrade: " + upgradeName);
        UpgradeManager.Instance.DespawnUpgrades();

        if (upgradeType == UpgradeManager.UpgradeType.normal || upgradeType == UpgradeManager.UpgradeType.detrimental)
        {
            AudioManager.PlayAudioClip(UpgradeManager.Instance.normalUpgradeSelection);
        }
        else
        {
            AudioManager.PlayAudioClip(UpgradeManager.Instance.specialUpgradeSelection);
        }

        
    }
}
