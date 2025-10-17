using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    [SerializeField] private List<Upgrade> upgrades = new List<Upgrade>();
    [Header("Upgrade UI")]
    [SerializeField] private GameObject spawningUpgradeParent;
    [Tooltip("The negative and positive of this value are the bounds that upgrades can spawn at")]
    [SerializeField] private float upgradeSpawningRange;
    [SerializeField] private float upgradeSpawningY;

    [SerializeField] private GameObject upgradePrefab;
    [SerializeField] private int spawningUpgradeAmount;

    private List<GameObject> spawnedUpgrades = new List<GameObject>();

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("More than 2 instances of the UpgradeManager class were found! Deleting duplicate");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadUpgrades(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadUpgrades()
    {
        string upgradeObjectsPath = "Upgrades";

        Upgrade[] loadedUpgrades = Resources.LoadAll<Upgrade>(upgradeObjectsPath);

        if (loadedUpgrades.Length <= 0)
        {
            Debug.LogWarning("No commands found in Resources/" + upgradeObjectsPath + " folder.");
            return;
        }

        foreach (Upgrade upgrade in loadedUpgrades)
        {
           if (upgrade.enabled)
           {
               upgrades.Add(upgrade);

               Debug.Log($"Loaded command: {upgrade.upgradeName}");
           }
        }
    }

    public void SpawnUpgrades()
    {
        List<Upgrade> spawningUpgrades = new List<Upgrade>();

        for (int i = 0; i < spawningUpgradeAmount; i++)
        {
            spawningUpgrades.Add(GetRandomUpgrade());
        }

        CreateUpgrades(spawningUpgrades);
    }

    private void CreateUpgrades(List<Upgrade> upgs)
    {
        for (int i = 0; i < upgs.Count; i++)
        {
            float distinceBetweenUpgrades = (upgradeSpawningRange * 2) / (upgs.Count - 1);

            Vector2 spawnPos = new(-upgradeSpawningRange + (distinceBetweenUpgrades * i), upgradeSpawningY);

            if (upgs.Count <= 1)
                spawnPos = new Vector2(0, upgradeSpawningY);

            GameObject spawnObj = Instantiate(upgradePrefab, spawningUpgradeParent.transform);

            UpgradeObject spawnObjUpgrade = spawnObj.GetComponent<UpgradeObject>();

            spawnObjUpgrade.upgrade = upgs[i];

            spawnObj.GetComponent<RectTransform>().localPosition = spawnPos;
            spawnedUpgrades.Add(spawnObj);
        }
    }
    
    public Upgrade GetRandomUpgrade()
    {
        while (true)
        {
            // get random upgrade in list
            Upgrade randomUpgrade = upgrades[Random.Range(0, upgrades.Count)];

            // use upgrade weight to check if it should add the upgrade
            float chance = Random.Range(0, 1);

            if (chance <= randomUpgrade.weight)
            {
                return randomUpgrade;
            }
        }
        
    }

    public void DespawnUpgrades()
    {
        if (spawnedUpgrades.Count <= 0) return;

        foreach (GameObject upg in spawnedUpgrades)
        {
            Destroy(upg);
        }
    }
}
