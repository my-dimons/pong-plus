using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeObject : MonoBehaviour
{
    public Upgrade upgrade;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        // todo : move to special function
        gameObject.name = "Upgrade: " + upgrade.upgradeName;
        //nameText.text = upgrade.upgradeName;
        descriptionText.text = upgrade.description;
        image.sprite = upgrade.image;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
