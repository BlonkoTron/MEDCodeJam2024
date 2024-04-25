using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Assuming you're using TextMesh Pro

public class Upgrades : MonoBehaviour
{
    public int upgradeCostBait = 3; // Base cost for upgrades
    public static int money; // Player's current money
    public int upgradeableBait = 4; // Flag to indicate if upgrades are available (optional)

    public TMP_Text goldText; // Text object to display money (using Text with capital T)
    public TMP_Text baitCostText;
    public TMP_Text fishingrodLevelText;

    public GameObject UICanvas;

    private int upgradeLevelBait;
    public Button upgradeBait; // Button to upgrade bait
    public AudioClip upgradeSound;

    [HideInInspector]
    public static bool doDisplayUI = true;

    private List<string> levelNames = new List<string>
    {
        "SUPERSONIC",
        "EPIIIC",
        "Great",
        "Better",
        "Normal",
    };

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = upgradeSound; // Assuming there's an AudioSource component attached

        upgradeBait.gameObject.SetActive(false);
    }

    void Update()
    {
        // Check if enough money and upgrades are available (optional)

        bool canUpgradeBait = money >= upgradeCostBait || upgradeableBait == 0; // Assuming non-nullable

        if (doDisplayUI)
        {
            UICanvas.SetActive(true);
            if (canUpgradeBait)
            {
                //Debug.Log("It thinks you can upgrade bait now");
                upgradeBait.gameObject.SetActive(true);
            }
            else
            {
                upgradeBait.gameObject.SetActive(false);
            }
        }
        else
        {
            UICanvas.SetActive(false);
        }

        // Update cost text if cost changes during gameplay
        baitCostText.text = "Cost: " + upgradeCostBait.ToString();
        goldText.text = "Gold: " + money.ToString();
        fishingrodLevelText.text = $"Fishing rod level: {levelNames[upgradeableBait]}";
        if (upgradeableBait == 0)
        {
            baitCostText.text = "Max";
        }

        
    }

    public void UpgradeBait() // Renamed for clarity
    {
        // Check if enough money and upgrade is available
        if (money >= upgradeCostBait && upgradeableBait > 0)
        {
            upgradeLevelBait++; // Increase bait level
            money -= upgradeCostBait; // Deduct cost from money
            upgradeCostBait *= 2;    // Increase cost for next upgrade
            upgradeableBait--;     // Reduce available upgrades (optional)
            Debug.Log("Bait upgraded to level " + upgradeLevelBait);
            // Play upgrade sound using AudioSource
            GetComponent<AudioSource>().Play(); // Assuming there's an AudioSource component attached
            return;
        }
        else
        {
            // Not enough money or no more upgrades
            Debug.Log("Not enough money or no more upgrades");
            return;
        }
    }
}

