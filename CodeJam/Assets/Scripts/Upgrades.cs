using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour 
{
 
    public static int Gold; // Player's current money
    public int money = Gold; // Gives the variable money the value of Gold'

    public int upgradeCostBait; // Base cost for upgrades
    public int upgradeableBait; // Flag to indicate if upgrades are available (optional)

    public TMP_Text goldText; // Text object to display money (using Text with capital T)
    public TMP_Text baitCostText; // Text object to display upgrade cost
    public TMP_Text fishingrodLevelText; // Text object to display upgrade level

    public GameObject UICanvas; // Reference to the UI Canvas

    private int upgradeLevelBait; // How many upgrades you have left before you reach max level
    public Button upgradeBait; // Button to upgrade bait'
    
    public AudioClip upgradeSound; // Sound to play when upgrading

    [HideInInspector]
    public static bool doDisplayUI = true; // Flag to show/hide UI

    private List<string> levelNames = new List<string> // List of upgrade names
  {
    "SUPERSONIC",
    "EPIIIC",
    "Great",
    "Better",
    "Normal",
  };

    // Reference to the FishingManager script
    public  FishingManager fishingManager;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = upgradeSound; // Assign upgrade sound to AudioSource
        upgradeBait.gameObject.SetActive(false); // Hide upgrade button
    }

    void Update()
    {
        // Check if enough money and upgrades are available

        bool canUpgradeBait = money >= upgradeCostBait || upgradeableBait == 0; // Check if you can upgrade bait

        if (doDisplayUI) 
        {
            UICanvas.SetActive(true); // Show UI
            if (canUpgradeBait) // Check if you can upgrade bait
            {
                upgradeBait.gameObject.SetActive(true); // Show upgrade button
            }
            else
            {
                upgradeBait.gameObject.SetActive(false); // Hide upgrade button
            }
        }
        else
        {
            UICanvas.SetActive(false); // Hide UI
        }

        // Update cost text if cost changes during gameplay
        baitCostText.text = "Cost: " + upgradeCostBait.ToString(); // Update cost text
        goldText.text = "Gold: " + money.ToString(); // Update money text
        fishingrodLevelText.text = $"Fishing rod level: {levelNames[upgradeableBait]}"; // Update level text
       
        if (upgradeableBait == 0) // Check if max level is reached
        {
            baitCostText.text = "Max"; // Display max if max level is reached
        }

    }

    public void UpgradeBait() // Renamed for clarity
    {
        // Check if enough money and upgrade is available
        if (money >= upgradeCostBait && upgradeableBait > 0)
        {
            upgradeLevelBait++; // Increase bait level
            money -= upgradeCostBait; // Deduct cost from money
            upgradeCostBait *= 2; // Increase cost for next upgrade by 2x
            upgradeableBait--; // Reduce available upgrades by 1
            Debug.Log("Bait upgraded to level " + upgradeLevelBait); // Log upgrade

            fishingManager.maxCatchTime -= 2; // Decrease maxCatchTime by 2 seconds from FishingManager
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
